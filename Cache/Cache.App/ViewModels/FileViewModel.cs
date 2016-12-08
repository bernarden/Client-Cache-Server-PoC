using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cache.WPF.Annotations;

namespace Cache.WPF.ViewModels
{
    public class FileViewModel : INotifyPropertyChanged
    {
        private bool _isCached;
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

        public bool IsCached
        {
            get { return _isCached; }
            set
            {
                _isCached = value;
                OnPropertyChanged(nameof(IsCached));
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
