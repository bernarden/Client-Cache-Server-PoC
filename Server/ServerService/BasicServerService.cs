using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Common;
using Server.Core;
using Server.Service.Dtos;
using Server.Service.Mappers;

namespace Server.Service
{
    /// <summary>
    /// Basic implementation of server's service
    /// </summary>
    public class BasicServerService : IServerService
    {
        /// <summary>
        /// Gets the file names.
        /// </summary>
        public IEnumerable<string> GetFileNames()
        {
            DirectoryInfo serverFilesDirectoryInfo = new DirectoryInfo(CommonConstants.ServerFilesLocation);
            return serverFilesDirectoryInfo.GetFiles().Select(x => x.FullName.Substring(CommonConstants.ServerFilesLocation.Length + 1));
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        public Stream DownloadFile(string fileName)
        {
            string downloadFilePath = Path.Combine(CommonConstants.ServerFilesLocation, $"{fileName}");

            if (!File.Exists(downloadFilePath))
            {
                throw new FileNotFoundException($"File with name {downloadFilePath} was not found");
            }
            return File.OpenRead(downloadFilePath);
        }

        /// <summary>
        /// Determines whether cache version of the file is up to date.
        /// </summary>
        public FileCurrentVersionStatus IsCurrentVersionOfFile(string fileName, string hashOfCachedFile)
        {
            string downloadFilePath = Path.Combine(CommonConstants.ServerFilesLocation, $"{fileName}");
            if (!File.Exists(downloadFilePath))
            {
                return FileCurrentVersionStatus.Removed;
            }
            using (StreamReader r = new StreamReader(downloadFilePath))
            {
                string fileContent = r.ReadToEnd();
                if (fileContent.CalculateSha256Hash().ToLower().Equals(hashOfCachedFile.ToLower()))
                {
                    return FileCurrentVersionStatus.UpToDate;
                }
            }
            return FileCurrentVersionStatus.Modified;
        }

        /// <summary>
        /// Gets the updated chunks.
        /// </summary>
        public IEnumerable<DifferenceChunkDto> GetUpdatedChunks(string fileName, IEnumerable<CachedChunkDto> cahceChunks)
        {
            string downloadFilePath = Path.Combine(CommonConstants.ServerFilesLocation, $"{fileName}");
            List<Chunk> chunks;

            using (StreamReader r = new StreamReader(downloadFilePath))
            {
                string fileContent = r.ReadToEnd();
                chunks = RabinKarpAlgorithm.Slice(fileContent).ToList();
            }

            return ChunkDifferentiator.GetUpdatedChunks(chunks, cahceChunks.Select(CachedChunkDtoMapper.Map).ToList()).Select(DifferenceChunkDtoMapper.Map);
        }
    }
}