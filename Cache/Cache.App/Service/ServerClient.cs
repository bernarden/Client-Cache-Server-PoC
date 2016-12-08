﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
            string cachedFileLocation = Path.Combine(CommonConstants.CacheFilesLocation, $"{fileName}");
            bool fileWasCached = CommonFunctionality.DoesFileExist(cachedFileLocation);

            if (fileWasCached)
            {
                byte[] allCachedBytes = System.IO.File.ReadAllBytes(cachedFileLocation);
                FileCurrentVersionStatus fileCurrentVersionStatus;
                using (ServerServiceClient serverServiceClient = new ServerServiceClient())
                {
                    fileCurrentVersionStatus = await serverServiceClient.IsCurrentVersionOfFileAsync(fileName, allCachedBytes.CalculateSha256Hash());

                    if (fileCurrentVersionStatus == FileCurrentVersionStatus.Modified)
                    {
                        return await UpdateCachedFile(fileName, allCachedBytes, serverServiceClient, cachedFileLocation);
                    }
                }

                if (fileCurrentVersionStatus == FileCurrentVersionStatus.UpToDate)
                {
                    return new MemoryStream(allCachedBytes);
                }

                throw new FileNotFoundException();
            }

            // File hasn't been cached before.
            using (ServerServiceClient serverServiceClient = new ServerServiceClient())
            {
                Stream downloadedFile = await serverServiceClient.DownloadFileAsync(fileName);
                SaveToNewFile(downloadedFile, cachedFileLocation);

                MainWindowViewModel mainWindowViewModel = IocKernel.Get<MainWindowViewModel>();
                mainWindowViewModel.Files.First(x => x.Name.Equals(fileName)).IsCached = true;

                return CommonFunctionality.GetMemoryStreamOfTheFile(cachedFileLocation);
            }
        }

        private async Task<Stream> UpdateCachedFile(string fileName, byte[] fileContent, ServerServiceClient serverServiceClient, string cachedFileLocation)
        {
            List<Chunk> chunks = RabinKarpAlgorithm.Slice(fileContent);
            DifferenceChunkDto[] differenceChunkDtos = await serverServiceClient.GetUpdatedChunksAsync(fileName, chunks.Select(CachedChunkDtoMapper.Map).ToArray());
            string newFileContent = ConstructContentOfTheUpdateFile(differenceChunkDtos, chunks);
            System.IO.File.WriteAllText(cachedFileLocation, newFileContent);
            byte[] byteArray = Encoding.UTF8.GetBytes(newFileContent);
            return new MemoryStream(byteArray);
        }

        private static string ConstructContentOfTheUpdateFile(DifferenceChunkDto[] differenceChunkDtos, List<Chunk> chunks)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DifferenceChunkDto chunkDto in differenceChunkDtos.OrderBy(x => x.CurentFileChunkNumber))
            {
                if ((chunkDto.ChunkInformation == null || chunkDto.ChunkInformation.Length == 0) && chunkDto.CachedFileChunkNumber > 0)
                {
                    builder.Append(chunkDto.ChunkInformation);
                }
                else
                {
                    builder.Append(chunks.First(x => x.FileChunkNumber == chunkDto.CachedFileChunkNumber).ChunkInformation);
                }
            }
            return builder.ToString();
        }

        private void SaveToNewFile(Stream downloadedFile, string filePath)
        {
            if (!Directory.Exists(CommonConstants.CacheFilesLocation))
                Directory.CreateDirectory(CommonConstants.CacheFilesLocation);

            using (var file = System.IO.File.Create(filePath))
            {
                downloadedFile.CopyTo(file);
            }
        }
    }
}
