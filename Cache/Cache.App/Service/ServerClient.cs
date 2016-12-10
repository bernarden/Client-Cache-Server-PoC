using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cache.WPF.ServerReference;
using Cache.WPF.Service.Mappers;
using Cache.WPF.ViewModels;
using Common;

namespace Cache.WPF.Service
{
    /// <summary>
    /// Responsible for communicating with server.
    /// </summary>
    public class ServerFileClient
    {
        private readonly FileManager _fileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerFileClient"/> class.
        /// </summary>
        public ServerFileClient(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// Gets the file names.
        /// </summary>
        public async Task<IEnumerable<string>> GetFileNames()
        {
            string[] fileNames;
            using (ServerServiceClient serverServiceClient = new ServerServiceClient())
            {
                fileNames = await serverServiceClient.GetFileNamesAsync();
            }

            UpdateFilesOnUi(fileNames);

            return fileNames;
        }

        private void UpdateFilesOnUi(string[] newFileNames)
        {
            MainWindowViewModel mainWindowViewModel = IocKernel.Get<MainWindowViewModel>();

            foreach (string fileName in newFileNames)
            {
                if (!mainWindowViewModel.Files.Any(x => x.Name.Equals(fileName)))
                {
                    mainWindowViewModel.Files.Add(new FileViewModel { Name = fileName, IsCached = false });
                }
            }

            for (int i = mainWindowViewModel.Files.Count - 1; i >= 0; i--)
            {
                FileViewModel file = mainWindowViewModel.Files[i];
                if (!newFileNames.Any(x => x.Equals(file.Name)))
                {
                    mainWindowViewModel.Files.Remove(file);
                    if (file.IsCached)
                    {
                        _fileManager.DeleteFile(file.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        public async Task<Stream> DownloadFile(string fileName)
        {
            Log($"{fileName} is requested by the client.");
            string cachedFileLocation = Path.Combine(CommonConstants.CacheFilesLocation, $"{fileName}");
            bool fileWasCached = CommonFunctionality.DoesFileExist(cachedFileLocation);

            if (fileWasCached)
            {
                Log($"{fileName} was previously cached.");
                byte[] allCachedBytes = System.IO.File.ReadAllBytes(cachedFileLocation);
                FileCurrentVersionStatus fileCurrentVersionStatus;
                using (ServerServiceClient serverServiceClient = new ServerServiceClient())
                {
                    fileCurrentVersionStatus = await serverServiceClient.IsCurrentVersionOfFileAsync(fileName, allCachedBytes.CalculateSha256Hash());

                    if (fileCurrentVersionStatus == FileCurrentVersionStatus.Modified)
                    {
                        Log($"{fileName} has been modified on the main server.");
                        Stream downloadFile = await UpdateCachedFile(fileName, allCachedBytes, serverServiceClient, cachedFileLocation);
                        MarkFileAsCached(fileName);
                        return downloadFile;
                    }
                }

                if (fileCurrentVersionStatus == FileCurrentVersionStatus.UpToDate)
                {
                    MarkFileAsCached(fileName);
                    return new MemoryStream(allCachedBytes);
                }

                throw new FileNotFoundException();
            }

            // File hasn't been cached before.
            using (ServerServiceClient serverServiceClient = new ServerServiceClient())
            {
                Log($"{fileName} hasn't been cached before. Downloading it from the main server for the first time.");
                Stream downloadedFile = await serverServiceClient.DownloadFileAsync(fileName);
                byte[] fileContent = CommonFunctionality.ReadFully(downloadedFile);
                SaveToNewFile(fileContent, cachedFileLocation);
                MarkFileAsCached(fileName);
                return new MemoryStream(fileContent);
            }
        }

        private async Task<Stream> UpdateCachedFile(string fileName, byte[] cachedFileContent, ServerServiceClient serverServiceClient, string cachedFileLocation)
        {
            List<Chunk> cachedChunks = RabinKarpAlgorithm.Slice(cachedFileContent);
            DifferenceChunkDto[] differenceChunkDtos = await serverServiceClient.GetUpdatedChunksAsync(fileName, cachedChunks.Select(CachedChunkDtoMapper.Map).ToArray());
            byte[] newFileContent = ConstructContentOfTheUpdateFile(differenceChunkDtos, cachedChunks, fileName);
            System.IO.File.WriteAllBytes(cachedFileLocation, newFileContent);
            return new MemoryStream(newFileContent);
        }

        private byte[] ConstructContentOfTheUpdateFile(DifferenceChunkDto[] differenceChunkDtos, List<Chunk> cachedChunks, string fileName)
        {
            long numberOfCachedBytesUsedDuringReconstruction = 0;
            List<byte[]> newFileContent = new List<byte[]>();
            foreach (DifferenceChunkDto chunkDto in differenceChunkDtos.OrderBy(x => x.CurentFileChunkNumber))
            {
                if (chunkDto.ChunkInformation != null && chunkDto.ChunkInformation.Length != 0 && chunkDto.CachedFileChunkNumber < 0)
                {
                    newFileContent.Add(chunkDto.ChunkInformation);
                }
                else
                {
                    Chunk chunk = cachedChunks.First(x => x.FileChunkNumber == chunkDto.CachedFileChunkNumber);
                    numberOfCachedBytesUsedDuringReconstruction += chunk.ChunkInformation.Length;
                    newFileContent.Add(chunk.ChunkInformation);
                }
            }

            byte[] enumerable = newFileContent.SelectMany(x => x).ToArray();
            Log($"{fileName} was reconstructed with {numberOfCachedBytesUsedDuringReconstruction * 100L / enumerable.Length * 1L}% cached data.");

            return enumerable;
        }

        private static void MarkFileAsCached(string fileName)
        {
            MainWindowViewModel mainWindowViewModel = IocKernel.Get<MainWindowViewModel>();
            FileViewModel file = mainWindowViewModel.Files.FirstOrDefault(x => x.Name.Equals(fileName));
            if (file != null)
                file.IsCached = true;
        }

        private void SaveToNewFile(byte[] fileContent, string filePath)
        {
            if (!Directory.Exists(CommonConstants.CacheFilesLocation))
                Directory.CreateDirectory(CommonConstants.CacheFilesLocation);

            System.IO.File.WriteAllBytes(filePath, fileContent);
        }

        private void Log(string logMessage)
        {
            MainWindowViewModel mainWindowViewModel = IocKernel.Get<MainWindowViewModel>();
            mainWindowViewModel.LogMessages.Add(new LogMessageViewModel { LogTime = DateTime.Now, Message = logMessage });
        }
    }
}
