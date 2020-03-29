namespace WindowsParty.Ui.ViewModels
{
    using Microsoft.Extensions.DependencyInjection;

    public class ViewModelLocator
    {
        public LogInViewModel LogInViewModel => App.ServiceProvider.GetRequiredService<LogInViewModel>();

        public ServersViewModel ServersViewModel => App.ServiceProvider.GetRequiredService<ServersViewModel>();

        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}