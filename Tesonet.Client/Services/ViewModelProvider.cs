using Tesonet.Client.ViewModels;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.Services
{
    public class ViewModelProvider : IViewModelProvider
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();

        public MainViewModel MainViewModel => _locator.MainViewModel;
        public LoginViewModel LoginViewModel => _locator.LoginViewModel;
        public MainPageViewModel MainPageViewModel => _locator.MainPageViewModel;
        public NavigationToolBarViewModel NavigationToolBarViewModel => _locator.NavigationToolBarViewModel;
        public ServersPageViewModel ServersPageViewModel => _locator.ServersPageViewModel;
        public SettingsPageViewModel SettingsPageViewModel => _locator.SettingsPageViewModel;
        public ErrorPageViewModel ErrorPageViewModel => _locator.ErrorPageViewModel;
    }
}