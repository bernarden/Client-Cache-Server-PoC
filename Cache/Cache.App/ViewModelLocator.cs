namespace Cache.App
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => IocKernel.Get<MainWindowViewModel>();
    }
}