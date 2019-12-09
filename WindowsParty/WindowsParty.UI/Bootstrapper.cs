using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WindowsParty.Api;
using WindowsParty.Data.DbContexts;
using WindowsParty.Data.Repositories;
using WindowsParty.Logger.Loggers;
using WindowsParty.UI.Services;
using WindowsParty.UI.ViewModels;

namespace WindowsParty.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LoginViewModel>();
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IWindowsLogger, ConsoleLogger>()
                .Singleton<ITokenService, TokenService>()
                .Singleton<IServerListService, ServerListService>()
                .Singleton<IUserValidationService, UserValidationService>()
                .PerRequest<IMainViewModel, MainViewModel>()
                .PerRequest<ILoginViewModel, LoginViewModel>()
                .PerRequest<UserContexts>()
                .PerRequest<IUserRepository, UserRepository>()
                .PerRequest<MainViewModel>()
                .PerRequest<LoginViewModel>();

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
