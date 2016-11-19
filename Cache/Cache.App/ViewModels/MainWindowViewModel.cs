using System.Collections.Generic;
using Cache.WPF.Service;

namespace Cache.WPF.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly FileManager _fileManager;

        public MainWindowViewModel( FileManager fileManager)
        {
            _fileManager = fileManager;
           // WcfServiceManager.StartService();
        }

        public List<FileViewModel> Files { get; set; }

        public List<LogMessageViewModel> LogMessages { get; set; }

        public void ClearCache()
        {
            _fileManager.CLearCache();
            Files.ForEach(x => x.IsCached = false);
        }

        public void UpdateListOfFiles(object sender, List<string> e)
        {
            throw new System.NotImplementedException();
        }
    }
}