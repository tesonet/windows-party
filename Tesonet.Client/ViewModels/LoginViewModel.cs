using System;
using System.Security;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Tesonet.Client.Extensions;
using Tesonet.Client.Helpers;
using Tesonet.Client.Properties;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels.Base;
using Tesonet.ServerProxy.Exceptions;
using Tesonet.ServerProxy.Services.AuthorizationService;

namespace Tesonet.Client.ViewModels
{
    public class LoginViewModel : NavigableViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISettings _settings;

        private string _userName;
        private SecureString _userPassword;

        private RelayCommand<object> _loginCommand;
        private RelayCommand _gotoSettingsCommand;

        public LoginViewModel(INavigationService navigationService, IAuthorizationService authorizationService, ISettings settings) : base(navigationService)
        {
            _authorizationService = authorizationService;
            _settings = settings;
        }

        public string UserName
        {
            get => _userName;
            set { _userName = value; RaisePropertyChanged(); }
        }

        public SecureString UserPassword
        {
            get => _userPassword;
            set { _userPassword = value; RaisePropertyChanged(); }
        }

        public RelayCommand<object> LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand<object>(Login));
        public RelayCommand GotoSettingsCommand => _gotoSettingsCommand ?? (_gotoSettingsCommand = new RelayCommand(GotoSettings));

        private async void Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                UserPassword = passwordContainer.Password;
            }

            await ExecuteBusyActionAsync(LoginAsync, Resources.Login);
        }

        private async Task LoginAsync()
        {
            try
            {
                Log.Info(Resources.Login);

                var token = await _authorizationService.GetAuthToken(_settings.ServerAuthUrl, UserName, UserPassword.ToUnsecureString());

                if (string.IsNullOrEmpty(token))
                {
                    Log.Error(Resources.NoAuthTokenRecieved);
                    await NavigationService.NavigateToErrorPageAsync(new ErrorPageNavigationData
                    {
                        ErrorTitle = Resources.LoginFailed,
                        ErrorMessage = Resources.LoginFailed,
                        NavigatedFromPage = this
                    });
                }
                else
                {
                    Log.Info(Resources.LoggedInSuccessfully, UserName);

                    _settings.AuthToken = token;
                    await NavigationService.NavigateToServersPageAsync(null);
                }
            }
            catch (ServiceAuthenticationException ex)
            {
                Log.Error(ex, Resources.LoginFailedForUser, UserName);

                await NavigationService.NavigateToErrorPageAsync(new ErrorPageNavigationData
                {
                    ErrorTitle = Resources.LoginFailed,
                    ErrorMessage = ex.Content,
                    NavigatedFromPage = this
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, Resources.LoginFailedForUser, UserName);

                await NavigationService.NavigateToErrorPageAsync(new ErrorPageNavigationData
                {
                    ErrorTitle = Resources.LoginFailed,
                    ErrorMessage = ex.Message,
                    NavigatedFromPage = this
                });
            }
        }

        private async void GotoSettings()
        {
            Log.Info(Resources.NavigateToSettingsPage);

            _settings.AuthToken = string.Empty;

            await ExecuteBusyActionAsync(GotoSettingsAsync, Resources.NavigateToSettingsPage);
        }

        private async Task GotoSettingsAsync()
        {
            try
            {
                await NavigationService.NavigateToSettingsPageAsync(null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, Resources.NavigateToSettingsPageFailed);

                await NavigationService.NavigateToErrorPageAsync(new ErrorPageNavigationData
                {
                    ErrorTitle = Resources.NavigateToSettingsPage,
                    ErrorMessage = ex.Message,
                    NavigatedFromPage = this
                });
            }
        }
    }
}