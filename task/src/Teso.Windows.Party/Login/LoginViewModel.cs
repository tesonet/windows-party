using System;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Teso.Windows.Party.Clients.Authentication;
using Teso.Windows.Party.Events;

namespace Teso.Windows.Party.Login
{
    public class LoginViewModel: Screen
    {
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyOfPropertyChange(Username);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public bool LoginEnabled
        {
            get => _loginEnabled;
            set
            {
                _loginEnabled = value;
                NotifyOfPropertyChange(() => LoginEnabled);
            }
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly IAuthenticationClient _authenticationClient;
        private ILog _logger;
        private string _username;
        private string _password;
        private bool _loginEnabled;

        public LoginViewModel(IEventAggregator eventAggregator, IAuthenticationClient authenticationClient)
        {
            _eventAggregator = eventAggregator;
            _authenticationClient = authenticationClient;
            _logger = LogManager.GetLog(GetType());
        }

        protected override void OnActivate()
        {
            ResetInputs();
            LoginEnabled = true;
            base.OnActivate();
        }

        public async Task Login(string username, string password)
        {
            LoginEnabled = false;

            try
            {
                AuthenticationResponse authenticationResponse = await _authenticationClient.Authenticate(
                    new AuthenticationRequest
                    {
                        Username = username,
                        Password = password
                    });

                _eventAggregator.PublishOnUIThread(new ChangeEvent(ChangeAction.LoggedIn, authenticationResponse.Token));
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Athentication failed!", String.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoginEnabled = true;
            }

            _logger.Info("User logged in");
        }

        private void ResetInputs()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}