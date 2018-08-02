using System.Threading.Tasks;
using System.Windows.Input;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.services.Authentication;
using tesonet.windowsparty.services.Navigation;
using tesonet.windowsparty.wpfapp.Commands;
using tesonet.windowsparty.wpfapp.Factories;
using tesonet.windowsparty.wpfapp.Navigation;

namespace tesonet.windowsparty.wpfapp.ViewModels
{
    public class LoginViewModel : BaseNotification, ILoginViewModel
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPasswordServiceFactory _passwordServiceFactory;
        private Credentials _credentials;
        private string _error;

        public LoginViewModel(INavigator navigator, IAuthenticationService authenticationService, IPasswordServiceFactory passwordServiceFactory)
        {
            _credentials = new Credentials();
            _passwordServiceFactory = passwordServiceFactory;
            _authenticationService = authenticationService;
            _navigator = navigator;
            _navigator.SubscribeToNavigationItem<ILoginViewModel, IToLoginView>(this, OnNavigationToLoginView);

            LoginCommand = new AsyncCommand(CanLoginAction, LoginAction);
        }

        public string Username
        {
            get
            {
                return _credentials.Username;
            }
            set
            {
                _credentials.Username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        public bool CanLogin
        {
            get { return true; }
        }

        public ICommand LoginCommand { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        private bool CanLoginAction()
        {
            return CanLogin;
        }

        private async Task LoginAction()
        {
            ErrorMessage = string.Empty;

            try
            {
                _credentials.Password = _passwordServiceFactory.PasswordService.Password;
                var token = await _authenticationService.Authenticate(_credentials);

                _navigator.PublishNavigationItem(new FromLoginView { Token = token });
            }
            catch (AuthenticationServiceException e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void OnNavigationToLoginView(IToLoginView toLoginView)
        {
            Clear();
        }

        private void Clear()
        {
            Username = string.Empty;
            _passwordServiceFactory.PasswordService.ClearPassword();
        }
    }
}
