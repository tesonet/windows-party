using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WindowsParty.Helpers;
using WindowsParty.Interfaces;
using WindowsParty.Logging;
using WindowsParty.ViewModels;
using Xceed.Wpf.Toolkit;

namespace WindowsParty
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _simpleContainer;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _simpleContainer = new SimpleContainer();

            _simpleContainer.Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAuthenticationHelper, AuthenticationHelper>()
                .Singleton<IWebTasks, WebTasks>();


            _simpleContainer.PerRequest<ShellViewModel>()
                .PerRequest<LoginViewModel>()
                .PerRequest<ServerListViewModel>();

            ConventionManager.AddElementConvention<WatermarkPasswordBox>(PasswordBoxHelper.BoundPasswordProperty, "Password", "PasswordChanged");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            LogManager.GetLog = type => new DebugLogger(type);
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
    }
}
