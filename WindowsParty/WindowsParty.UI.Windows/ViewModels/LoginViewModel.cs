using System;
using WindowsParty.Core.External.PlaygroundClient;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Services;
using Caliburn.Micro;

namespace WindowsParty.UI.Windows.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly ITokenService _tokenService;
        private readonly IEventAggregator _eventAggregator;

        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(LoginViewModel));

        private bool _canLogin;
        private string _password;
        private string _username;
        private bool _isAuthenticating;
        private string _errorMsg;

        public LoginViewModel(ITokenService tokenService, IEventAggregator eventAggregator)
        {
            _tokenService = tokenService;
            _eventAggregator = eventAggregator;
        }


        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value)
                    return;

                _username = value;
                NotifyOfPropertyChange(() => Username);
                Validate();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                _password = value;
                NotifyOfPropertyChange(() => Password);
                Validate();
            }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                if (_password == value)
                    return;

                _errorMsg = value;
                NotifyOfPropertyChange(() => ErrorMsg);
                Validate();
            }
        }

        public bool IsAuthenticating
        {
            get { return _isAuthenticating; }
            set
            {
                if (_isAuthenticating == value)
                    return;

                _isAuthenticating = value;
                NotifyOfPropertyChange(() => IsAuthenticating);
            }
        }

        public bool CanLogin
        {
            get { return _canLogin; }
            set
            {
                if (_canLogin == value)
                    return;

                _canLogin = value;
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public async void Login()
        {
            IsAuthenticating = true;
            try
            {
                var response = await _tokenService.GetToken(new TokenRequest(Username, Password));
                if (!string.IsNullOrEmpty(response.Token))
                {
                    Password = Username = ErrorMsg = null;

                    await _eventAggregator.PublishOnUIThreadAsync(response);
                }
            }
            catch (ClientException ex)
            {
                _log.Warn(ex.Message, ex);
                ErrorMsg = "Incorrect username or password";
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
            }
            finally
            {
                IsAuthenticating = false;
            }
        }

        private void Validate()
        {
            CanLogin = !string.IsNullOrEmpty(Username) & !string.IsNullOrEmpty(Password);
        }
    }
}
