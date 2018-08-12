using Tesonet.Client.ViewModels;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.Services
{
    public interface IViewModelProvider
    {
        MainViewModel MainViewModel { get; }
        LoginViewModel LoginViewModel { get; }
        MainPageViewModel MainPageViewModel { get; }
        NavigationToolBarViewModel NavigationToolBarViewModel { get; }
        ServersPageViewModel ServersPageViewModel { get; }
        SettingsPageViewModel SettingsPageViewModel { get; }
        ErrorPageViewModel ErrorPageViewModel { get; }
    }
}