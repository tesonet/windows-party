using WinPartyArs.Views;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;
using WinPartyArs.Abstracts;
using WinPartyArs.Models;
using Prism.Logging;
using System;
using WinPartyArs.Common;

namespace WinPartyArs
{
    class Bootstrapper : UnityBootstrapper
    {
        public Bootstrapper()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                Logger?.Log($"AppDomain.CurrentDomain.UnhandledException sender '{sender}', exception: {args?.ExceptionObject}", Category.Exception);
        }

        protected override DependencyObject CreateShell() => Container.Resolve<MainWindow>();

        protected override void InitializeShell() => Application.Current.MainWindow.Show();

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Logger.Log("Registering instances", Category.Debug);
            Container.RegisterInstance<TesonetServiceAbstract>(Container.Resolve<TesonetService>());

            Logger.Log("Registering types for navigation", Category.Debug);
            Container.RegisterTypeForNavigation<Login>();
            Container.RegisterTypeForNavigation<ServerList>();
        }

        protected override ILoggerFacade CreateLogger() => new PrismLog4NetProxy();

        internal void OnExit(ExitEventArgs e) => Logger.Log("Bootstrapper OnExit()", Category.Info);
    }
}
