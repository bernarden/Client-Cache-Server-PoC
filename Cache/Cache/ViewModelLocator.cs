namespace Cache
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => IocKernel.Get<MainWindowViewModel>();
    }
}