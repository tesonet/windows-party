using Autofac;
using System.Configuration;
using Tesonet.Windows.Party.Repositories;
using Tesonet.Windows.Party.Services;
using Tesonet.Windows.Party.ViewModels;

namespace Tesonet.Windows.Party
{
    public static class Bootstrapper
    {
        public static IContainer Container { get; private set; }

        static Bootstrapper()
        {
            var containerBuilder = new ContainerBuilder();

            var apiUrl = ConfigurationManager.AppSettings["BaseApiUrl"];

            containerBuilder
                .Register(f => new AuthService(apiUrl))
                .As<IAuthService>()
                .SingleInstance();

            containerBuilder
                .Register(f => new ServerRepository(apiUrl))
                .As<IServerRepository>()
                .SingleInstance();

            containerBuilder.Register(f => new LogInViewModel(f.Resolve<IAuthService>()));
            containerBuilder.Register(f => new ServerListViewModel(f.Resolve<IServerRepository>()));
            containerBuilder.RegisterType<MainViewModel>();


            Container = containerBuilder.Build();
        }
    }
}
