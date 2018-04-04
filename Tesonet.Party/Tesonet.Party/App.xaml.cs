using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tesonet.Party.ExceptionHandling;
using Tesonet.Party.Services;
using Tesonet.Party.ViewModels;
using Unity;
using Unity.Lifetime;

namespace Tesonet.Party
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = new UnityContainer();
            container.RegisterType<ITesonetServiceAgent, TesonetServiceAgent>();
            container.RegisterType<ISessionService, SessionService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IShellService, ShellVM>(new ContainerControlledLifetimeManager());
            container.Resolve<Shell>().Show();
        }

        private static void Application_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptionHandler.HandleException(e.Exception, true);
            e.Handled = true;
        }
    }
}
