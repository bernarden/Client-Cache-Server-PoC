using Cache.Service;

namespace Cache.App
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(IWcfServiceManager wcfServiceManager)
        {
            wcfServiceManager.StartService();
        }
    }
}
