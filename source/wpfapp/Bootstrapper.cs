using Serilog;
using System;
using tesonet.windowsparty.caching;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.data;
using tesonet.windowsparty.data.Servers;
using tesonet.windowsparty.data.Tokens;
using tesonet.windowsparty.http;
using tesonet.windowsparty.logging;
using tesonet.windowsparty.services;
using tesonet.windowsparty.services.Authentication;
using tesonet.windowsparty.services.Navigation;
using tesonet.windowsparty.services.Servers;
using tesonet.windowsparty.wpfapp.Factories;
using tesonet.windowsparty.wpfapp.ViewModels;
using tesonet.windowsparty.wpfapp.Views;
using Unity;
using Unity.Injection;
using ILogger = tesonet.windowsparty.logging.ILogger;
using ISerilogLogger = Serilog.ILogger;

namespace tesonet.windowsparty.wpfapp
{
    public static class Bootstrapper
    {
        public static IUnityContainer Container { get; private set; }

        public static void Initialize()
        {
            var container = new UnityContainer();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    "log.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();


            container.RegisterInstance<ISerilogLogger>(logger);
            container.RegisterSingleton<ILogger, Logger>();
            container.RegisterSingleton<ICache<TokenResponse>, Cache<TokenResponse>>();
            container.RegisterType<IClient, LoggingClient>(new InjectionConstructor(new Client(@"http://playground.tesonet.lt/v1"), container.Resolve<ILogger>()));
            container.RegisterType<IQueryHandler<Credentials, TokenResponse>, StartedFinishedLoggingQueryHandler<Credentials, TokenResponse>>(
                new InjectionConstructor(
                    new LoggingTokenQueryHandler(
                        new CachingTokenQueryHandler(
                            new TokenQueryHandler(container.Resolve<IClient>()),
                            container.Resolve<ICache<TokenResponse>>()),
                        container.Resolve<ILogger>()),
                    container.Resolve<ILogger>()));

            container.RegisterType<IQueryHandler<string, Server[]>, StartedFinishedLoggingQueryHandler<string, Server[]>>(
                new InjectionConstructor(
                    new LoggingServerQueryHandler(
                        new ServerQueryHandler(container.Resolve<IClient>()), container.Resolve<ILogger>()),
                    container.Resolve<ILogger>()));

            container.RegisterType<IAuthenticationService, LoggingAuthenticationService>(
                new InjectionConstructor(
                    new AuthenticationService(container.Resolve<IQueryHandler<Credentials, TokenResponse>>()), container.Resolve<ILogger>()));

            container.RegisterType<IServersService, LoggingServersService>(
                new InjectionConstructor(
                    new ServersService(container.Resolve<IQueryHandler<string, Server[]>>()), container.Resolve<ILogger>()));

            container.RegisterSingleton<INavigator, Navigator>();
            container.RegisterSingleton<ILoginView, LoginView>();
            container.RegisterType<ILoginViewModel, LoginViewModel>();
            container.RegisterSingleton<IServersView, ServersView>();
            container.RegisterType<IServersViewModel, ServersViewModel>();
            container.RegisterSingleton<IMainWindow, MainWindow>();
            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
            container.RegisterInstance<IPasswordServiceFactory>(new PasswordServiceFactory(() => container.Resolve<ILoginView>()));

            Container = container;
        }
    }
}
