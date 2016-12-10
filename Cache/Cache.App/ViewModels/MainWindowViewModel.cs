using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cache.WPF.Annotations;
using Cache.WPF.Service;

namespace Cache.WPF.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private readonly FileManager _fileManager;
        private ObservableCollection<FileViewModel> _files;
        private ObservableCollection<LogMessageViewModel> _logMessages;

        public MainWindowViewModel(FileManager fileManager)
        {
            _fileManager = fileManager;
            Files = new ObservableCollection<FileViewModel>();
            LogMessages = new ObservableCollection<LogMessageViewModel>();
        }

        public ObservableCollection<FileViewModel> Files
        {
            get { return _files; }
            set
            {
                _files = value;
                OnPropertyChanged(nameof(Files));
            }
        }

        public ObservableCollection<LogMessageViewModel> LogMessages
        {
            get { return _logMessages; }
            set
            {
                _logMessages = value;
                OnPropertyChanged(nameof(LogMessages));

            }
        }

        public void ClearCache()
        {
            _fileManager.CLearCache();
            foreach (var file in Files)
            {
                file.IsCached = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}