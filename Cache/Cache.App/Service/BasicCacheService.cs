using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cache.WPF.Service
{
    /// <summary>
    /// Basic implementation of cache's services
    /// </summary>
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

        //public delegate void MessageEventHandler(List<string> newFiles);
        //public event MessageEventHandler UpdateUiWithNewFiles;

        /* void SendMessage(List<string> newFiles)
         {
             UpdateUiWithNewFiles?.Invoke(new List<string>());
         }*/


        public event EventHandler<List<string>> CustomEvent;

        public void SendData(int value)
        {
            CustomEvent?.Invoke(null, new List<string>());
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
