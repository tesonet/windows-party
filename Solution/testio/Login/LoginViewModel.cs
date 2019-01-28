using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using testio.Core.Services.AuthenticationService;
using testio.HandleMessages.Navigation;
using testio.Core.Logging;
using testio.Caliburn;

namespace testio.Login
{
    public class LoginViewModel: BaseScreen
    {
        #region Events

        // pk: Security issue - ugly code to avoid plain text password property
        public event Func<string> HarvestPassword;

        #endregion Events

        #region Fields

        private IAuthenticationService _authenticationService = null;
        private IEventAggregator _eventAggregator = null;

        private string _username = null;

        #endregion Fields        

        #region Constructors

        public LoginViewModel(IAuthenticationService authenticationService, IEventAggregator eventAggregator, ILogger logger):
            base(logger)
        {
            _authenticationService = authenticationService;
            _eventAggregator = eventAggregator;

//#if DEBUG
//            Username = "tesonet";
//#endif
        }

        #endregion Constructors

        public async void Login()
        {
            try
            {
                SetBusy();

                var authenticationResult = await _authenticationService.Authenticate(Username, HarvestPassword());
                switch (authenticationResult.Result)
                {
                    case AuthenticationResultType.Success:
                        _logger.LogInfoFormat("[{0}] logged in", Username);
                        _eventAggregator.PublishOnUIThread(new NavigationMessage(TargetWindow.ServerList));
                        break;

                    case AuthenticationResultType.EmailOrPasswordIsIncorrect:
                        Error = "Invalid login";
                        _logger.LogWarningFormat("[{0}] invalid login", Username);
                        break;

                    case AuthenticationResultType.Error:
                        Error = authenticationResult.Error.Message;
                        _logger.LogErrorFormat(authenticationResult.Error, "[{0}] failed to log", Username);
                        break;
                }
            }
            catch (Exception e)
            {
                ProcessException(e);
            }
            finally
            {
                SetNotBusy();
            }
        }

        protected override void OnBusyChanged()
        {
            base.OnBusyChanged();
            NotifyOfPropertyChange(() => CanLogin);
        }

        internal void OnPasswordChanged()
        {
            NotifyOfPropertyChange(() => CanLogin);
        }

        #region Properties

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin
        {
            get
            {
                return !String.IsNullOrEmpty(_username) && 
                    !String.IsNullOrEmpty(HarvestPassword()) && 
                    !IsBusy;
            }
        }

        #endregion Properties
    }
}
