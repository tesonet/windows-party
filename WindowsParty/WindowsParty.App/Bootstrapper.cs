using Caliburn.Micro;
using Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Windows;
using WindowsParty.App.Domain;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Handlers;
using WindowsParty.App.Services;
using WindowsParty.App.Services.Models;
using WindowsParty.App.ViewModels;

namespace WindowsParty.App
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
            _container = new SimpleContainer();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .CreateLogger();

            var api = RestService.For<IPlaygroundApi>("http://playground.tesonet.lt");

            _container.RegisterInstance(typeof(IPlaygroundApi), null, api);
            _container.RegisterInstance(typeof(ILogger), null, logger);

            Log.Information("Hello, Serilog!");

            Log.CloseAndFlush();

            _container.Singleton<IPlaygroundClient, PlaygroundClient>();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<ICommandProcessor, CommandProcessor>();
            _container.Singleton<ITokenService, TokenService>();

            _container.RegisterPerRequest(typeof(ICommandHandler<LoginUserCommand>), null, typeof(LoginUserCommandHandler));
            _container.RegisterPerRequest(typeof(ICommandHandler<GetServersCommand>), null, typeof(GetServersCommandHandler));

            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<LoginViewModel>();
            _container.PerRequest<ServerViewModel>();
        }


        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
            {
                return instance;
            }
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
