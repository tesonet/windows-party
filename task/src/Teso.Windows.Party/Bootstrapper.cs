using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Teso.Windows.Party.Clients.Authentication;
using Teso.Windows.Party.Clients.ServerList;
using Teso.Windows.Party.Configuration;
using Teso.Windows.Party.Logging;
using Teso.Windows.Party.Login;
using Teso.Windows.Party.Models;
using Teso.Windows.Party.ServerList;
using Teso.Windows.Party.Shell;
using Xceed.Wpf.Toolkit;

namespace Teso.Windows.Party
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;
        private IConfiguration _configuration;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _configuration = new Configuration.Configuration();

            _container = new SimpleContainer();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<User>();

            _container.RegisterInstance(typeof(IAuthenticationClient), "AuthenticationClient", CreateAuthenticationClient());
            _container.RegisterInstance(typeof(IServerListClient), "ServerListClient", CreateServerListClient());
            _container
                .PerRequest<ShellViewModel>()
                .PerRequest<LoginViewModel>()
                .PerRequest<ServerListViewModel>();

            LogManager.GetLog = type => new Logger(type);

            ConventionManager.AddElementConvention<WatermarkPasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
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

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Xceed.Wpf.Toolkit.MessageBox.Show(Application.MainWindow, e.Exception.Message, String.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private IAuthenticationClient CreateAuthenticationClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.BaseUrl)
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return new AuthenticationClient(httpClient, jsonSerializerSettings);
        }

        private IServerListClient CreateServerListClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.BaseUrl)
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return new ServerListClient(httpClient, jsonSerializerSettings);
        }
    }
}