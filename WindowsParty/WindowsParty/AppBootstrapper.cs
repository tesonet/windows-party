using Autofac;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WindowsParty.Clients;
using WindowsParty.Clients.Contracts;
using WindowsParty.Services;
using WindowsParty.Services.Contracts;
using WindowsParty.ViewModels;
namespace WindowsParty
{
    public class AppBootstrapper : BootstrapperBase
    {
        public static IContainer Container { get; private set; }
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(App));
        public AppBootstrapper() => Initialize();

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ConductorViewModel>();
        }

        protected override void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started application");
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(FatalExceptionHandler);

            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<ApiClient>().As<IApiClient>();
            builder.RegisterType<SessionService>().As<ISessionService>().SingleInstance();
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<ConductorViewModel>().SingleInstance();
            builder.RegisterType<ServerListViewModel>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();

            Container = builder.Build();
            Container.InjectProperties(Container);
        }

        protected override object GetInstance(Type serviceType, string key) => Container.Resolve(serviceType);

        static void FatalExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            log.Fatal($"Application exited with exception", ex);
        }
    }
}
