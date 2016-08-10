using Cache.Service;

namespace Cache
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(IWcfServiceManager wcfServiceManager)
        {
            wcfServiceManager.StartService();
        }
    }
}
