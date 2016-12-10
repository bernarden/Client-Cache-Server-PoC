using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client.Service
{
    public class FileManager
    {
        public void CLearCache()
        {
            if (!Directory.Exists(CommonConstants.ClientFilesLocation))
            {
                Directory.CreateDirectory(CommonConstants.ClientFilesLocation);
            }
            else
            {
                var allFiles = Directory.GetFiles(CommonConstants.ClientFilesLocation, "*.*", SearchOption.AllDirectories);
                Parallel.ForEach(allFiles, DeleteFileWithFullPath);
            }
        }
        
        public void SaveToNewFile(Stream downloadedFile, string fileName)
        {
            if (!Directory.Exists(CommonConstants.ClientFilesLocation))
                Directory.CreateDirectory(CommonConstants.ClientFilesLocation);

            string pathToNewFile = Path.Combine(CommonConstants.ClientFilesLocation, fileName);
            using (var file = File.Create(pathToNewFile))
            {
                downloadedFile.CopyTo(file);
            }
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
}