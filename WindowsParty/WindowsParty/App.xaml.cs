using Autofac;
using Autofac.Core;
using Autofac.Extensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WindowsParty.Clients;
using WindowsParty.Clients.Contracts;
using WindowsParty.Services;
using WindowsParty.Services.Contracts;
using WindowsParty.ViewModels;

namespace WindowsParty
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }
        private static ILog log = LogManager.GetLogger(typeof(App));
        public App()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Started application");
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(FatalExceptionHandler);

            var builder = new ContainerBuilder();
            builder.Register(v => { return log; }).SingleInstance();
            builder.RegisterType<ApiClient>().As<IApiClient>();
            builder.RegisterType<SessionService>().As<ISessionService>().SingleInstance();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<ServerListViewModel>();
            Container = builder.Build();
            Container.InjectProperties(Container);
        }

        static void FatalExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            log.Fatal($"Application exited with exception", ex);
        }
    }
}
