using System.Windows;
using Cache.Service;
using Ninject;
using Ninject.Modules;

namespace Cache
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
     protected override void OnStartup(StartupEventArgs eventArgs)
        {
            base.OnStartup(eventArgs);
            IocKernel.Initialize(new IocConfiguration());
        }

    }
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IWcfServiceManager>().To<WcfServiceManager>().InSingletonScope();
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
        }
    }

    public static class IocKernel
    {
        private static StandardKernel _kernel;

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modules);
            }
        }
    }

}
