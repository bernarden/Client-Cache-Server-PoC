using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Client.CacheService;
using Client.Service;

namespace Client.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private readonly ICacheService _cacheService;
        private readonly FileManager _fileManager;
        private bool _operationInProgress;
        private FileViewModel _selectedFile;

        public MainWindowViewModel(FileManager fileManager, ICacheService cacheService)
        {
            _fileManager = fileManager;
            _cacheService = cacheService;

            //  Files = _cacheService.GetFileNames().Select(x => new FileViewModel { Name = x, IsCached = false }).ToList();
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


        //public List<FileViewModel> Files { get; private set; }

        public List<FileViewModel> Files
        {
            get
            {
                return new List<FileViewModel>
                {
                    new FileViewModel {Name = "name3", IsCached = false},
                    new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
                };
            }
            set { }
        }


        public string DownloadOpenFileButtonText
        {
            get
            {
                if ((SelectedFile == null) || !SelectedFile.IsCached)
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
            // Downloading file 
            // => IsClearDownloadsButtonEnabled = true 
        }

        public void RefreshList()
        {
            OperationInProgress = true;

            var fileNames = _cacheService.GetFileNames();
            var newFiles = new List<FileViewModel>();
            foreach (var fileName in fileNames)
            {
                var fileViewModel =
                    Files.FirstOrDefault(x => string.Equals(x.Name, fileName, StringComparison.OrdinalIgnoreCase));
                newFiles.Add(fileViewModel ?? new FileViewModel { Name = fileName, IsCached = false });
            }
            Files = newFiles;

            OperationInProgress = false;
        }

        public void ClearDownloads()
        {
            OperationInProgress = true;

            _fileManager.CLearCache();
            Files.ForEach(x => x.IsCached = false);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}