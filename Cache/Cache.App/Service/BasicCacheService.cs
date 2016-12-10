using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using Cache.WPF.ViewModels;

namespace Cache.WPF.Service
{
    /// <summary>
    /// Basic implementation of cache's services
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class BasicCacheService : ICacheService
    {
        private readonly ServerFileClient _fileClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCacheService"/> class.
        /// </summary>
        public BasicCacheService()
        {
            _fileClient = IocKernel.Get<ServerFileClient>();
        }

        /// <summary>
        /// Gets the file names.
        /// </summary>
        public async Task<IEnumerable<string>> GetFileNames()
        {
            return await _fileClient.GetFileNames();
        }
        
        /// <summary>
        /// Downloads the file.
        /// </summary>
        public async Task<Stream> DownloadFile(string fileName)
        {
            return await _fileClient.DownloadFile(fileName);

        }
    }
}
