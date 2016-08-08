using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using Server.Service.Dtos;

namespace Server.Service
{
    /// <summary>
    /// Responsible for providing servers' services
    /// </summary>
    [ServiceContract]
    public interface IServerService
    {
        /// <summary>
        /// Gets the file names.
        /// </summary>
        [OperationContract]
        IEnumerable<string> GetFileNames();

        /// <summary>
        /// Downloads the file.
        /// </summary>
        [OperationContract]
        Stream DownloadFile(string fileName);

        /// <summary>
        /// Determines whether cache version of the file is up to date.
        /// </summary>
        [OperationContract]
        bool IsCurrentVersionOfFile(string fileName, string hashOfCachedFile);

        /// <summary>
        /// Gets the updated chunks.
        /// </summary>
        [OperationContract]
        IEnumerable<DifferenceChunkDto> GetUpdatedChunks(string fileName, IEnumerable<CachedChunkDto> cahceChunks);
    }
}