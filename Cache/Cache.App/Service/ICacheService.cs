using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cache.WPF.Service
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
        Task<IEnumerable<string>> GetFileNames();

        /// <summary>
        /// Downloads the file.
        /// </summary>
        [OperationContract]
        Task<Stream> DownloadFile(string fileName);
    }
}
