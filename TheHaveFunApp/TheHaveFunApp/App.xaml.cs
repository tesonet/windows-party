using System;
using System.Windows;
using CommonServiceLocator;
using log4net;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using TheHaveFunApp.Services;
using TheHaveFunApp.Services.Interfaces;
using TheHaveFunApp.ViewModels;
using TheHaveFunApp.Views;

namespace TheHaveFunApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(App));

        public override void Initialize()
        {
            base.Initialize();

            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.AddToRegion("MainRegion", this.Container.Resolve<LoginPage>());

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            _log.Info("        =============  Started Logging  =============        ");
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ServersListPage, ServersListPageViewModel>();
            containerRegistry.RegisterSingleton<IHttpService, HttpService>();
            containerRegistry.RegisterSingleton<ILogService, LogService>();

            this.Container.Resolve<ILogService>()?.Init(_log);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                this.Container.Resolve<ILogService>()?.LogException(exception);
                MessageBox.Show(exception.Message);
            }
            catch
            {

            }
        }
    }
}
