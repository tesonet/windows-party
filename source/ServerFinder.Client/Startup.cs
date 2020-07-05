namespace ServerFinder.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;
    using System.Windows;
    using Caliburn.Micro;
    using Serilog;
    using ApplicationShell;
    using Integration;
    using Login;
    using ServersList;

    public class Startup: BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        
        public Startup()
        {
            base.Initialize();
        }

        protected override void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application starting.");

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            var apiConfig = (Hashtable)ConfigurationManager.GetSection("apiConfig");
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiConfig["baseUrl"].ToString());
            _container.Handler<HttpClient>((args) => httpClient);
            
            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<LoginViewModel>();
            _container.PerRequest<ServersListViewModel>();

            _container.Singleton<IServiceClient, ServiceClient>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
