using System.Collections.Generic;

namespace Cache.WPF.ViewModels
{
    public interface IMainWindowViewModel
    {
        List<FileViewModel> Files { get; }
        List<LogMessageViewModel> LogMessages { get; }
        void ClearCache();
    }
}