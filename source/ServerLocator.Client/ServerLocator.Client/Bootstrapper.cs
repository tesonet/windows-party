using Caliburn.Micro;
using ServerLocator.Client.ServerClient;
using ServerLocator.Client.Shared;
using ServerLocator.Client.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Windows;

namespace ServerLocator.Client
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            container.Singleton<ILog, Logger>();

            var apiSection = (Hashtable)ConfigurationManager.GetSection("apiConfig");
            var apiConfig = new ClientApiConfig
            {
                BaseUrl = apiSection["baseUrl"].ToString(),
                ServersEndpoint = apiSection["serversEndpoint"].ToString(),
                TokensEndpoint = apiSection["tokensEndpoint"].ToString()
            };
            container.Handler<ClientApiConfig>((args) => apiConfig);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiConfig.BaseUrl);
            container.Handler<HttpClient>((args) => httpClient);

            container.PerRequest<ShellViewModel>();
            container.PerRequest<LoginViewModel>();
            container.PerRequest<ServerListViewModel>();

            this.container.Singleton<IServerClient, ServerClient.ServerClient>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
