﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Common;
using Server.Core;

namespace ServerService
{
    /// <summary>
    /// Basic implementaiton of server service
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
        public bool IsCurrentVersionOfFile(string fileName, string hashOfCachedFile)
        {
            string downloadFilePath = Path.Combine(CommonConstants.ServerFilesLocation, $"{fileName}");
            using (StreamReader r = new StreamReader(downloadFilePath))
            {
                string fileContent = r.ReadToEnd();
                if (fileContent.CalculateSha256Hash().Equals(hashOfCachedFile))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the updated chunks.
        /// </summary>
        public IEnumerable<DifferenceChunk> GetUpdatedChunks(string fileName, IEnumerable<CachedChunkDto> cahceChunks)
        {
            string downloadFilePath = Path.Combine(CommonConstants.ServerFilesLocation, $"{fileName}");
            List<Chunk> chunks;

            using (StreamReader r = new StreamReader(downloadFilePath))
            {
                string fileContent = r.ReadToEnd();
                chunks = RabinKarpAlgorithm.Slice(fileContent, ChunkOriginType.CurrentFile).ToList();
            }

            return ChunkDifferentiator.GetDifferenceChunks(chunks, cahceChunks.Select(ChunkMapper.Map).ToList());
        }
    }
}