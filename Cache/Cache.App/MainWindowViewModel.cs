using System.Collections.Generic;
using Cache.Service;

namespace Cache.App
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public MainWindowViewModel(IWcfServiceManager wcfServiceManager)
        {
            wcfServiceManager.StartService();
        }

        public List<FileViewModel> Files { get; }
        public List<LogMessageViewModel> LogMessages { get; }

        public void ClearCache()
        {
            //TODO for each cached file call model to delete files.
        }
    }
}