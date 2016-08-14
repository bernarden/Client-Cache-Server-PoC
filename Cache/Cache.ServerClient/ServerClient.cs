using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cache.Common;
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
                        // TODO Split file into chunks, get hashes and send them off to the server to evaluate. 

                    }
                }
                
                if (fileCurrentVersionStatus == FileCurrentVersionStatus.UpToDate)
                {
                    return CommonFunctionality.GetMemoryStreamOfTheFile(fileName);
                }
                else if (fileCurrentVersionStatus == FileCurrentVersionStatus.Removed)
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                using (ServerServiceClient serverServiceClient = new ServerServiceClient())
                {
                    Stream downloadedFile = await serverServiceClient.DownloadFileAsync(fileName);
                    SaveToFile(downloadedFile, cachedFileLocation);
                    // TODO Update UI. This file was saved.
                    return downloadedFile;
                }
            }

        }

        private void SaveToFile(Stream downloadedFile, string filePath)
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
