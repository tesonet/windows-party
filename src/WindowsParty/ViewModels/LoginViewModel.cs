using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using WindowsParty.Events;
using WindowsParty.Interfaces;
using WindowsParty.Models;

namespace WindowsParty.ViewModels
{
    public class LoginViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IAuthenticationHelper _authenticationHelper;
        private string _username, _password, _errorMessage;

        public LoginViewModel(IEventAggregator eventAggregator, IAuthenticationHelper authenticationHelper)
        {
            //Initialize interfaces
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _authenticationHelper = authenticationHelper ?? throw new ArgumentNullException(nameof(authenticationHelper));
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                //If property changed - notify dependants
                NotifyOfPropertyChange(() => Username);
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

        public bool ErrorDisplayed
        {
            get => ErrorMessage?.Length > 0 ? true : false;
            set
            {
                NotifyOfPropertyChange(() => ErrorDisplayed);
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorDisplayed);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        internal AuthModel AuthModel { get; private set; }

        public async Task AuthUser()
        {
            try
            {
                //Check if input is valid
                if(validateInput())
                {
                    //Input is valid - create a user model object
                    UserModel userModel = new UserModel(Username, Password);

                    var authModel = await _authenticationHelper.AuthenticateUser(userModel);

                    if(!string.IsNullOrEmpty(authModel.AuthToken))
                    {
                        AuthModel = authModel;
                        _eventAggregator.PublishOnUIThread(new EventModel(Status.Signin));
                    }                    
                }
                else
                {
                    ErrorMessage = "Invalid input";
                }
            }
            catch (TaskCanceledException ex)
            {
                LogManager.GetLog(this.GetType()).Info("Authorization was cancelled: {0}", ex);
            }
        }

        internal bool validateInput()
        {
            //Check if the user has typed anything in
            //If more details would've been given - could have used regex to filter out incorrect formats
            return Username?.Length > 0 && Password?.Length > 0 ? true : false;
        }
    }
}
