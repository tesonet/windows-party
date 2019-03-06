using System;
using System.Windows;
using CommonServiceLocator;
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ServersListPage, ServersListPageViewModel>();
            containerRegistry.RegisterSingleton<IHttpService, HttpService>();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception)?.Message);
        }
    }
}
