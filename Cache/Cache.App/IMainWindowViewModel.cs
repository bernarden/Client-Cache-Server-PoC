using System.Collections.Generic;

namespace Cache.App
{
    public interface IMainWindowViewModel
    {
        List<FileViewModel> Files { get; }
        List<LogMessageViewModel> LogMessages { get; }
        void ClearCache();
    }
}