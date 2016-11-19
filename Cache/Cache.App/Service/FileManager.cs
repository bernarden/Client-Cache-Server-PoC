using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Cache.WPF.Service
{
    public class FileManager
    {
        public void CLearCache()
        {
            var allFiles = Directory.GetFiles(CommonConstants.CacheFilesLocation, "*.*", SearchOption.AllDirectories);
            Parallel.ForEach(allFiles, DeleteFileWithFullPath);
        }

        private void DeleteFileWithFullPath(string filePath)
        {
            // This might never delete a file on it's own, if there are always request coming in for specified file
            // Perhaps scheduling mechanism should be implemented. 
            FileInfo file = new FileInfo(filePath);
            while (IsFileLocked(file))
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
            file.Delete();
        }

        public void DeleteFile(string filePath)
        {
            string fileFullPath = Path.Combine(CommonConstants.CacheFilesLocation, $"{filePath}");
            DeleteFileWithFullPath(fileFullPath);
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
            return false;
        }
    }

    class File
    {
        private string ActualFileName;
        private string FileNameOnCachedServer;
        private bool isLatestVersion;
        private int NumberOfCurrentUsers;

        public File(string actualFileName, string fileNameOnCachedServer, bool isLatestVersion, int numberOfCurrentUsers)
        {
            ActualFileName = actualFileName;
            FileNameOnCachedServer = fileNameOnCachedServer;
            this.isLatestVersion = isLatestVersion;
            NumberOfCurrentUsers = numberOfCurrentUsers;
        }
    }
}
