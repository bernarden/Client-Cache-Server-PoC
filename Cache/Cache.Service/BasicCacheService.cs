using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cache.Common;

namespace Cache.Service
{
    /// <summary>
    /// Basic implementation of cache's services
    /// </summary>
    public class BasicCacheService : ICacheService
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
    }
}
