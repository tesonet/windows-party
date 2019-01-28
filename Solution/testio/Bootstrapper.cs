using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using testio.Shell;
using testio.Core.Services.AuthenticationService;
using testio.Core.Services.ServersService;
using testio.Core.Logging;
using testio.Services.AuthenticationService;
using testio.Services.ServersService;
using System.Windows.Threading;

namespace testio
{
    public class Bootstrapper: BootstrapperBase
    {
        #region Fields

        private SimpleContainer _container = null;

        #endregion Fields

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IAuthenticationService, AuthenticationService>();
            _container.Singleton<IServersService, ServersService>();

            log4net.Config.XmlConfigurator.Configure();
            _container.Singleton<ILogger, testio.Logging.log4net.Logger>();

            _container.PerRequest<Login.LoginViewModel>();
            _container.PerRequest<ServerList.ServerListViewModel>();
            _container.PerRequest<ShellViewModel>();
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
            MessageBox.Show(Application.MainWindow, e.Exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
