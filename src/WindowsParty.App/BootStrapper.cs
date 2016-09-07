using System;
using System.Configuration;
using System.Windows;
using WindowsParty.Infrastructure;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Navigation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using RestSharp;

namespace WindowsParty.App
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            var address = ConfigurationManager.AppSettings["WebApiAddress"];
            var restClient = new RestClient(address);
            Container.RegisterType<IAuthenticator, Authenticator>(new InjectionConstructor(restClient));
            Container.RegisterType<IServerListProvider, ServerListProvider>(new InjectionConstructor(restClient));
            
            Container.RegisterType<ITitleResolver, TitleResolver>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INavigator, Navigator>();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(Regions.MainRegion, new Uri(AppViews.LoginView, UriKind.Relative));

        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(MainModule.MainModule));
            return catalog;
        }
    }
}