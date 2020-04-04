using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using WPFApp.Interfaces;
using WPFApp.Services;
using WPFApp.ViewModels;


namespace WPFApp
{
    public class Bootstrapper: BootstrapperBase
    {
        private readonly SimpleContainer _simpleContainer;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Bootstrapper()
        {
            _simpleContainer = new SimpleContainer();
            Initialize();
        }

        protected override void Configure()
        {
            _simpleContainer.Instance(_simpleContainer);

            _simpleContainer.Singleton<IWindowManager, WindowManager>();
            _simpleContainer.Singleton<IEventAggregator, EventAggregator>();
            _simpleContainer.Singleton<IHttpService, HttpService>();
            _simpleContainer.Singleton<ITokenService, TokenService>();
            _simpleContainer.Singleton<ShellViewModel>();
            _simpleContainer.PerRequest<LoginViewModel>();
            _simpleContainer.PerRequest<ServerListViewModel>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            log.Info("        =============  Party started  =============        ");
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _simpleContainer.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _simpleContainer.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _simpleContainer.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
           e.Handled = true;
           log.Info($"An error has occurred: { e.Exception.Message}");
        }
    }
}
