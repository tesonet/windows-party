using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Caliburn.Micro;
using WindowsParty.App.Configurations;
using WindowsParty.App.Interfaces;
using WindowsParty.App.Services;
using WindowsParty.App.ViewModels;

namespace WindowsParty.App
{
    public class AppBootstrapper : BootstrapperBase 
    {
        private readonly SimpleContainer _container;

        public AppBootstrapper() 
        {
            _container = new SimpleContainer();
            Initialize();
        }

        protected override void Configure() 
        {
            _container.Instance(_container);

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            
            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<LoginViewModel>();
            _container.PerRequest<ServerViewModel>();
            _container.PerRequest<DashboardViewModel>();

            _container.PerRequest<IUserService, UserService>();
            _container.PerRequest<IServersDataService, ServersDataService>();

            var apiConfiguration = new HttpDataConfiguration()
            {
                ApiUrlBase = ConfigurationManager.AppSettings[nameof(HttpDataConfiguration.ApiUrlBase)] ?? throw new ArgumentNullException(nameof(HttpDataConfiguration.ApiUrlBase)),
                Version = ConfigurationManager.AppSettings[nameof(HttpDataConfiguration.Version)] ?? throw new ArgumentNullException(nameof(HttpDataConfiguration.Version))
            };
            _container.Instance(apiConfiguration);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _container.Instance(httpClient);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) 
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) 
        {
             _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) 
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        //protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        //{
        //    throw new NotImplementedException();

        //    //e.Handled = true;
        //    //MessageBox.Show(e.Exception.Message, "An error as occurred", MessageBoxButton.OK);
        //}
    }
}
