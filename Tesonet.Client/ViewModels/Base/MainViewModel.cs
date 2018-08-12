using Tesonet.Client.Helpers;
using Tesonet.Client.Services.NavigationService;
using Tesonet.ServerProxy.Services.AuthorizationService;

namespace Tesonet.Client.ViewModels.Base
{
    public class MainViewModel : NavigableViewModel
    {
        private NavigableViewModel _mainPage;

        public MainViewModel(INavigationService navigationService, IAuthorizationService authorizationService, ISettings settings) : base(navigationService)
        {
            MainPage = new LoginViewModel(navigationService, authorizationService, settings);
        }
       
        public NavigableViewModel MainPage
        {
            get => _mainPage;
            set
            {
                _mainPage = value;
                RaisePropertyChanged();
            }
        }
    }
}