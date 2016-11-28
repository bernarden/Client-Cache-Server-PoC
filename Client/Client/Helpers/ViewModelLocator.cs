using Client.ViewModels;

namespace Client.Helpers
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => IocKernel.Get<MainWindowViewModel>();
    }
}