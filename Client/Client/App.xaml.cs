using System.Windows;
using Client.Helpers;

namespace Client
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
}
