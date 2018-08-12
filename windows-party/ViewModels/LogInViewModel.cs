using System;
using System.Net.Http;
using System.Reflection;
using log4net;
using Tesonet.Windows.Party.Helpers;
using Tesonet.Windows.Party.Models;
using Tesonet.Windows.Party.Services;

namespace Tesonet.Windows.Party.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string Username { private get; set; }
        public string Password { private get; set; }

        private bool _showErrorMessage; 
        public bool ShowErrorMessage
        {
            get => _showErrorMessage;
            set => SetProperty(ref _showErrorMessage, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public RelayCommand LogInCommand { get; private set; }

        public Action<User> LogInSuccessful = delegate { };

        private readonly IAuthService _authService;

        public LogInViewModel(IAuthService authService)
        {
            _authService = authService;
            LogInCommand = new RelayCommand(OnLogIn);
        }

        private async void OnLogIn()
        {
            try
            {
                ShowErrorMessage = false;
                var token = await _authService.LogIn(Username, Password);
                LogInSuccessful(new User(Username, token));
            }
            catch (HttpRequestException hex)
            {
                ShowErrorMessage = true;
                ErrorMessage = hex.Message;
                _log.Warn($"Failed to authenticate {Username}. Error: {hex.Message}", hex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage = true;
                ErrorMessage = "Unexpected error has occured";
                _log.Error("Unexpected error has occured.", ex);
            }
        }
    }
}
