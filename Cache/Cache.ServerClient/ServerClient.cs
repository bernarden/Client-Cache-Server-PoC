using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cache.Common;
using Cache.ServerClient.Mappers;
using Cache.ServerClient.ServiceReference;

namespace Cache.ServerClient
{
    /// <summary>
    /// Responsible for communicating with server.
    /// </summary>
    public class ServerFileClient
    {
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

            // TODO Update UI list of files;
            // TODO UI should delete downloaded files if they are no longer on server.

            return fileNames;
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
                string fileContent = GetFileContent(cachedFileLocation);
                FileCurrentVersionStatus fileCurrentVersionStatus;
                using (ServerServiceClient serverServiceClient = new ServerServiceClient())
                {
                    fileCurrentVersionStatus = await serverServiceClient.IsCurrentVersionOfFileAsync(fileName, fileContent.CalculateSha256Hash());

                    if (fileCurrentVersionStatus == FileCurrentVersionStatus.Modified)
                    {
                        return await UpdateCachedFile(fileName, fileContent, serverServiceClient, cachedFileLocation);
                    }
                }

                if (fileCurrentVersionStatus == FileCurrentVersionStatus.UpToDate)
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(fileContent);
                    return new MemoryStream(byteArray);
                }

                throw new FileNotFoundException();
            }

            // File hasn't been cached before.
            using (ServerServiceClient serverServiceClient = new ServerServiceClient())
            {
                Stream downloadedFile = await serverServiceClient.DownloadFileAsync(fileName);
                SaveToNewFile(downloadedFile, cachedFileLocation);
                // TODO Update UI. This file was saved.
                return downloadedFile;
            }
        }

        private async Task<Stream> UpdateCachedFile(string fileName, string fileContent, ServerServiceClient serverServiceClient,
            string cachedFileLocation)
        {
            List<Chunk> chunks = RabinKarpAlgorithm.Slice(fileContent);
            DifferenceChunkDto[] differenceChunkDtos = await serverServiceClient.GetUpdatedChunksAsync(fileName, chunks.Select(CachedChunkDtoMapper.Map).ToArray());
            string newFileContent = ConstructContentOfTheUpdateFile(differenceChunkDtos, chunks);
            File.WriteAllText(cachedFileLocation, newFileContent);
            byte[] byteArray = Encoding.UTF8.GetBytes(newFileContent);
            return new MemoryStream(byteArray);
        }

        private static string ConstructContentOfTheUpdateFile(DifferenceChunkDto[] differenceChunkDtos, List<Chunk> chunks)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DifferenceChunkDto chunkDto in differenceChunkDtos.OrderBy(x => x.CurentFileChunkNumber))
            {
                if (string.IsNullOrEmpty(chunkDto.ChunkInformation) && chunkDto.CachedFileChunkNumber > 0)
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
            using (var fileStream = File.Create(filePath))
            {
                downloadedFile.Seek(0, SeekOrigin.Begin);
                downloadedFile.CopyTo(fileStream);
            }
        }

        private string GetFileContent(string cachedFileLocation)
        {
            using (StreamReader streamReader = new StreamReader(cachedFileLocation))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
