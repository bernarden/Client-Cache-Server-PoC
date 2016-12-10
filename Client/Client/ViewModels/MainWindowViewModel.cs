using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Client.CacheService;
using Client.Service;
using Common;

namespace Client.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private readonly FileManager _fileManager;
        private bool _operationInProgress;
        private FileViewModel _selectedFile;
        private ObservableCollection<FileViewModel> _files;

        public MainWindowViewModel(FileManager fileManager)
        {
            _fileManager = fileManager;
            RefreshList();
        }

        public bool OperationInProgress
        {
            get { return _operationInProgress; }
            set
            {
                _operationInProgress = value;
                NotifyPropertyChanged(nameof(OperationInProgress));
            }
        }

        public ObservableCollection<FileViewModel> Files
        {
            get { return _files; }
            private set
            {
                _files = value;
                NotifyPropertyChanged(nameof(Files));

            }
        }

        public string DownloadOpenFileButtonText
        {
            get
            {
                if ((SelectedFile == null) || !SelectedFile.IsFileCached())
                    return "Download File";
                return "Open File";
            }
        }

        public FileViewModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                NotifyPropertyChanged(nameof(DownloadOpenFileButtonText));
                NotifyPropertyChanged(nameof(SelectedFile));
            }
        }

        public async void DownloadOpenFile()
        {
            OperationInProgress = true;
            if (SelectedFile != null)
            {
                var file = Files.FirstOrDefault(x => x.Name.Equals(SelectedFile.Name));
                if (file != null)
                {

                    if (!file.IsFileCached())
                    {
                        try
                        {
                            file.FileStatusType = FileStatusType.Downloading;
                            var stream = await new CacheServiceClient().DownloadFileAsync(file.Name);
                            _fileManager.SaveToNewFile(stream, file.Name);
                            file.FileStatusType = FileStatusType.Downloaded;
                        }
                        catch (Exception)
                        {
                            // Show error message
                            file.FileStatusType = FileStatusType.NotDownloaded;
                        }
                        NotifyPropertyChanged(nameof(DownloadOpenFileButtonText));
                    }
                    else
                    {
                        // Open file with default program
                        var pathToFile = Path.Combine(CommonConstants.ClientFilesLocation, file.Name);
                        System.Diagnostics.Process.Start(pathToFile);
                    }
                }
            }

            OperationInProgress = false;
        }

        public async void RefreshList()
        {
            OperationInProgress = true;

            try
            {
                var fileNames = await new CacheServiceClient().GetFileNamesAsync();
                var newFiles = new ObservableCollection<FileViewModel>();
                foreach (var fileName in fileNames)
                {
                    var fileViewModel = Files?.FirstOrDefault(x => string.Equals(x.Name, fileName, StringComparison.OrdinalIgnoreCase));
                    newFiles.Add(fileViewModel ?? new FileViewModel { Name = fileName, FileStatusType = FileStatusType.NotDownloaded });
                }
                Files = newFiles;
            }
            catch (Exception)
            {
                // Show error message
            }

            OperationInProgress = false;
        }


        public void ClearDownloads()
        {
            OperationInProgress = true;

            _fileManager.CLearCache();
            foreach (var file in Files)
            {
                file.FileStatusType = FileStatusType.NotDownloaded;
            }

            NotifyPropertyChanged(nameof(DownloadOpenFileButtonText));
            OperationInProgress = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}