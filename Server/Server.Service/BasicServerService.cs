using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
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

            byte[] allBytes = File.ReadAllBytes(downloadFilePath);
            return string.Equals(allBytes.CalculateSha256Hash(), hashOfCachedFile, StringComparison.OrdinalIgnoreCase)
                ? FileCurrentVersionStatus.UpToDate : FileCurrentVersionStatus.Modified;
        }

        /// <summary>
        /// Gets the updated chunks.
        /// </summary>
        public IEnumerable<DifferenceChunkDto> GetUpdatedChunks(string fileName, IEnumerable<CachedChunkDto> cacheChunks)
        {
            string downloadFilePath = Path.Combine(CommonConstants.ServerFilesLocation, $"{fileName}");
            List<Chunk> chunks = RabinKarpAlgorithm.Slice(File.ReadAllBytes(downloadFilePath));
            return ChunkDifferentiator.GetUpdatedChunks(cacheChunks.Select(CachedChunkDtoMapper.Map).ToList(), chunks).Select(DifferenceChunkDtoMapper.Map);
        }
    }
}