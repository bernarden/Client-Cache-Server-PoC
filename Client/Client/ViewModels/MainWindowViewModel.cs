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
        private readonly ICacheService _cacheService;
        private readonly FileManager _fileManager;
        private bool _operationInProgress;
        private FileViewModel _selectedFile;

        public MainWindowViewModel(FileManager fileManager, ICacheService cacheService)
        {
            _fileManager = fileManager;
            _cacheService = cacheService;

            Files = new ObservableCollection<FileViewModel>(_cacheService.GetFileNames().Select(x => new FileViewModel { Name = x, IsCached = false }));
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

        public ObservableCollection<FileViewModel> Files { get; private set; }

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
            OperationInProgress = true;
            if (SelectedFile != null)
            {
                var stream = await _cacheService.DownloadFileAsync(SelectedFile.Name);
                SaveToNewFile(stream, SelectedFile.Name);
            }

            OperationInProgress = false;
            // Downloading file 
            // => IsClearDownloadsButtonEnabled = true 
        }

        private void SaveToNewFile(Stream downloadedFile, string fileName)
        {
            if (!Directory.Exists(CommonConstants.ClientFilesLocation))
                Directory.CreateDirectory(CommonConstants.ClientFilesLocation);

            string pathToNewFile = Path.Combine(CommonConstants.ClientFilesLocation, fileName);
            using (var file = File.Create(pathToNewFile))
            {
                downloadedFile.CopyTo(file);
            }
        }



        public void RefreshList()
        {
            OperationInProgress = true;

            var fileNames = _cacheService.GetFileNames();
            var newFiles = new ObservableCollection<FileViewModel>();
            foreach (var fileName in fileNames)
            {
                var fileViewModel = Files.FirstOrDefault(x => string.Equals(x.Name, fileName, StringComparison.OrdinalIgnoreCase));
                newFiles.Add(fileViewModel ?? new FileViewModel { Name = fileName, IsCached = false });
            }
            Files = newFiles;

            OperationInProgress = false;
        }

        public void ClearDownloads()
        {
            OperationInProgress = true;

            _fileManager.CLearCache();
            foreach (var file in Files)
            {
                file.IsCached = false;
            }

            OperationInProgress = false;

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}