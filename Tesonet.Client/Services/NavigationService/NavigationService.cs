using System;
using System.Threading.Tasks;
using Tesonet.Client.ViewModels;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        private readonly IViewModelProvider _viewModelProvider;

        public NavigationService(IViewModelProvider viewModelProvider)
        {
            _viewModelProvider = viewModelProvider;
        }

        public async Task NavigateToPageAsync(NavigableViewModel navigationPage, NavigationData.NavigationData navigationData)
        {
            if (navigationPage is LoginViewModel)
            {
                await NavigateToLoginPageAsync(navigationData);
            }
            else
            {
                await NavigateToMainPageAsync(navigationData);
            }
        }

        public async Task NavigateToLoginPageAsync(NavigationData.NavigationData navigationData)
        {
            var mainViewModel = _viewModelProvider.MainViewModel;
            var loginViewodel = _viewModelProvider.LoginViewModel;
            mainViewModel.MainPage = loginViewodel;

            await loginViewodel.InitializeAsync(navigationData);
        }

        public async Task NavigateToMainPageAsync(NavigationData.NavigationData navigationData)
        {
            var mainViewModel = _viewModelProvider.MainViewModel;
            var mainPageViewModel = _viewModelProvider.MainPageViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            await mainPageViewModel.InitializeAsync(navigationData);
        }

        public async Task NavigateToServersPageAsync(NavigationData.NavigationData navigationData)
        {
            var mainPageViewModel = await SetupMainPage(navigationData);

            mainPageViewModel.NavigationToolBar.IsBusy = true;
            try
            {
                var serversViewModel = _viewModelProvider.ServersPageViewModel;
                mainPageViewModel.SelectedPage = serversViewModel;

                await serversViewModel.InitializeAsync(navigationData);
            }
            finally
            {
                mainPageViewModel.NavigationToolBar.IsBusy = false;
            }
        }

        public async Task NavigateToSettingsPageAsync(NavigationData.NavigationData navigationData)
        {
            var mainPageViewModel = await SetupMainPage(navigationData);

            mainPageViewModel.NavigationToolBar.IsBusy = true;
            try
            {
                var settingsViewModel = _viewModelProvider.SettingsPageViewModel;
                mainPageViewModel.SelectedPage = settingsViewModel;

                await settingsViewModel.InitializeAsync(navigationData);
            }
            finally
            {
                mainPageViewModel.NavigationToolBar.IsBusy = false;
            }
        }

        public async Task NavigateToErrorPageAsync(NavigationData.NavigationData navigationData)
        {
            var mainViewModel = _viewModelProvider.MainViewModel;
            var errorPageViewModel = _viewModelProvider.ErrorPageViewModel;
            mainViewModel.MainPage = errorPageViewModel;

            await errorPageViewModel.InitializeAsync(navigationData);
        }

        private async Task<MainPageViewModel> SetupMainPage(NavigationData.NavigationData navigationData)
        {
            var mainViewModel = _viewModelProvider.MainViewModel;
            var mainPageViewModel = _viewModelProvider.MainPageViewModel;

            if (mainViewModel.MainPage == mainPageViewModel)
            {
                return mainPageViewModel;
            }

            mainViewModel.MainPage = mainPageViewModel;
            await mainPageViewModel.InitializeAsync(navigationData);

            return mainPageViewModel;
        }
    }
}