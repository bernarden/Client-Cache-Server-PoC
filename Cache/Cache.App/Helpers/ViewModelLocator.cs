using Cache.WPF.ViewModels;

namespace Cache.WPF.Helpers
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => IocKernel.Get<MainWindowViewModel>();
    }
}