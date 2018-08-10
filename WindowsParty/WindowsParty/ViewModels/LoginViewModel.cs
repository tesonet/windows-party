using System.Linq;
using WindowsParty.IServices;
using Caliburn.Micro;

namespace WindowsParty.ViewModels
{
    public class LoginViewModel : Conductor<object>
    {
        private string _username = "Username";
        private string _password = "Password";

        private readonly IAuthorizationService _authorizationService;
        private readonly IServerService _serverService;

        public LoginViewModel(IAuthorizationService authorizationService, IServerService serverService)
        {
            _authorizationService = authorizationService;
            _serverService = serverService;
        }

        public BindableCollection<ServerViewModel> GetServers()
        {
            var accessToken = _authorizationService.GetAccessToken("tesonet", "partyanimal");
            var serverViewModels = new BindableCollection<ServerViewModel>(_serverService.GetServers(accessToken).Select(x => new ServerViewModel { Name = x.Name, Distance = x.Distance + "km" }));
            return serverViewModels;
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }
     
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
    }
}
