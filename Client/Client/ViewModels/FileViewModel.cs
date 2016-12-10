using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ViewModels
{
    public class FileViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));

            }
        }

        private FileStatusType _fileStatusType;
        public FileStatusType FileStatusType
        {
            get { return _fileStatusType; }
            set
            {
                _fileStatusType = value;
                OnPropertyChanged(nameof(FileStatusType));
            }
        }

        public bool IsFileCached()
        {
            return FileStatusType == FileStatusType.Downloaded;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}