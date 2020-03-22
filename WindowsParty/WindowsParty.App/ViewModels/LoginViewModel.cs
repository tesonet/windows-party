using Caliburn.Micro;
using System.Threading.Tasks;
using WindowsParty.App.Interfaces;

namespace WindowsParty.App.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public LoginViewModel(
            INavigationService navigationService,
            IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
        }

        private string _username;
        public string Username 
        {
            get { return _username; }
            set { Set(ref _username, value); } 
        }

        private string _password;
        public string Password 
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        public async Task Login()
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                // TODO: AddValidation popup?
            }

            var bearer = await _userService.GetToken(_username, _password);

            if (bearer == default)
            {
                // TODO: AddValidation popup?
            }

            UserSession.SetBearerToken(bearer.Token);
            _navigationService.NavigateToViewModel<DashboardViewModel>();
        }
    }
}
