using Client.CacheService;
using Client.Service;
using Client.ViewModels;
using Ninject.Activation.Caching;
using Ninject.Modules;

namespace Client.Helpers
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ICacheService>().To<CacheServiceClient>().InSingletonScope();
            Bind<FileManager>().To<FileManager>().InSingletonScope();
            Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
        }
    }
}
