using System;
using System.IO;

namespace Server.Core
{
    public class FileSystemWatcherManager
    {
        private readonly string _filesLocation;

        public FileSystemWatcherManager(string filesLocation)
        {
            _filesLocation = filesLocation;
        }

        public void SetupFileSystemWatcher()
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(_filesLocation)
            {
                EnableRaisingEvents = true
            };
            fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
            fileSystemWatcher.Created += FileSystemWatcherOnCreated;
            fileSystemWatcher.Renamed += FileSystemWatcherOnRenamed;
            fileSystemWatcher.Deleted += FileSystemWatcherOnDeleted;
        }

        private static void FileSystemWatcherOnDeleted(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            throw new NotImplementedException();
        }

        private static void FileSystemWatcherOnRenamed(object sender, RenamedEventArgs renamedEventArgs)
        {
            throw new NotImplementedException();
        }

        private static void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            throw new NotImplementedException();
        }

        private static void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}