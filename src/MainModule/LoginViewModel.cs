using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowsParty.Infrastructure;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Navigation;
using Prism.Commands;
using Prism.Regions;

namespace MainModule
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private bool _errorOccurred;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool ErrorOccurred
        {
            get { return _errorOccurred; }
            set
            {
                _errorOccurred = value; 
                OnPropertyChanged();
            }
        }


        public LoginViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            LoginCommand = new DelegateCommand(OnLogin);

            ErrorOccurred = false;
            //Username ="tesonet";
            //Password = "partyanimal";
        }

        private void OnLogin()
        {
            var statusCode = _authenticator.Authenticate(Username, Password);
            if (statusCode == HttpStatusCode.OK)
            {
                ErrorOccurred = false;
                var parameters = new NavigationParameters {{"token", _authenticator.Token}};
                _navigator.GoTo(AppViews.ServersView, parameters);
            }
            else
            {
                ErrorOccurred = true;
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
