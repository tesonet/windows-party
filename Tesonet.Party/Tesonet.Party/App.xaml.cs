using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tesonet.Party.Services;
using Unity;

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
            container.RegisterType<ISessionService, SessionService>();
            container.Resolve<MainWindow>().Show();
        }
    }
}
