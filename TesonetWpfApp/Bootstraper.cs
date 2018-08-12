using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using RestSharp;
using System.Windows;
using TesonetWpfApp.Business;
using TesonetWpfApp.Views;

namespace TesonetWpfApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();

            IRegionManager regionManager = Container.TryResolve<IRegionManager>();
            regionManager.RequestNavigate("ContentRegion", nameof(Login));
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            
            Container.RegisterType<ITesonetService, TesonetService>();
            Container.RegisterType<IRestService, RestService>();
            Container.RegisterType<IRestClient, RestClient>(new InjectionConstructor());

            Container.RegisterTypeForNavigation<Login>(nameof(Login));
            Container.RegisterTypeForNavigation<ServerList>(nameof(ServerList));
        }
    }
}
