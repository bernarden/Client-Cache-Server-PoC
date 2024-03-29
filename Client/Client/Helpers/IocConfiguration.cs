﻿using Client.Service;
using Client.ViewModels;
using Ninject.Modules;

namespace Client.Helpers
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<FileManager>().To<FileManager>().InSingletonScope();
            Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
        }
    }
}
