using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Caliburn.Micro;
using Serilog;
using WindowsPartyTest.Client.Base;
using WindowsPartyTest.Client.Services;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.ViewModels;
using WindowsPartyTest.ViewModels.Interfaces;
using WindowsPartyTest.Views;
using WindowsPartyTest.Views.Interfaces;

namespace WindowsPartyTest
{

    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            if (Execute.InDesignMode)
                return;

            _container = new SimpleContainer();
            _container.Instance(_container);
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            var val = ConfigurationManager.AppSettings["ConnectionUrl"];
            APIConfig config = new APIConfig();
            config.ConnectionUrl = ConfigurationManager.AppSettings["ConnectionUrl"];
            config.TokensEndPoint = ConfigurationManager.AppSettings["TokensEndPoint"];
            config.ServerEndPoint = ConfigurationManager.AppSettings["ServerEndPoint"];

            string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(logsDirectory, "log-{Date}.txt"))
                .CreateLogger();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(config.ConnectionUrl);

            _container.Handler<APIConfig>(c => config);
            _container.Handler<HttpClient>(c => client);

            _container.PerRequest<ILoginService, LoginClient>();
            _container.PerRequest<IServerService, ServerClient>();

            _container.PerRequest<IShellViewModel, ShellViewModel>();
            _container.PerRequest<ILoginConductorViewModel, LoginConductorViewModel>();
            _container.PerRequest<IContentConductorViewModel, ContentConductorViewModel>();

            _container.PerRequest<ILoginViewModel, LoginViewModel>();
            _container.PerRequest<IMainViewModel, MainViewModel>();
            _container.PerRequest<IHeaderViewModel, HeaderViewModel>();

            _container.PerRequest<ILoginHandler, LoginView>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
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
    }

}