using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace Cache.Service
{
    /// <summary>
    /// Defines cache's services
    /// </summary>
    [ServiceContract]
    public interface ICacheService
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
    }
}
