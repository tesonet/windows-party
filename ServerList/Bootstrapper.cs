using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using ServerList.Utils;
using ServerList.ViewModels;
using System.Net.Http;
using System.Windows;

namespace ServerList
{
    class Bootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public Bootstrapper()
        {
            LogManager.GetLog = type => new DebugLogger(type);
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(c => new HttpClient()).As<HttpClient>().SingleInstance();
            builder.RegisterType<DebugLogger>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AppConfig>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AuthenticationService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ServersService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ShellViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<ServersViewModel>().SingleInstance();
        }
    }
}
