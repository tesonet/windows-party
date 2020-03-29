namespace WindowsParty.Ui
{
    using System;
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using WindowsParty.Authentication.Tesonet;
    using WindowsParty.Repository.Tesonet;
    using WindowsParty.Ui.Services;
    using WindowsParty.Ui.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
               .ConfigureServices(ConfigureServices)
               .ConfigureLogging(
                    logging =>
                    {
                        logging.AddConsole();

                        // Add other loggers...
                    })
               .Build();

            ServiceProvider = _host.Services;
        }

        public static IServiceProvider ServiceProvider { get; private set; }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainView = ServiceProvider.GetRequiredService<MainView>();
            mainView.Show();

            var pageNavigationService = ServiceProvider.GetRequiredService<IPageNavigationService>();
            pageNavigationService.NavigateTo<LogInView>();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTesonetAuthentication();
            services.AddTesonetRepository();
            services.AddUiModule();
        }
    }
}