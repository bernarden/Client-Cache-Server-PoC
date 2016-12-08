using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cache.WPF.ViewModels
{
    public interface IMainWindowViewModel
    {
        ObservableCollection<FileViewModel> Files { get; }
        ObservableCollection<LogMessageViewModel> LogMessages { get; }
        void ClearCache();
    }
}