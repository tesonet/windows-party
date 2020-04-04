using Caliburn.Micro;
using System.Threading.Tasks;
using WPFApp.Interfaces;
using WPFApp.Models;
using Xceed.Wpf.Toolkit;

namespace WPFApp.ViewModels
{
    public class LoginViewModel: Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IHttpService _httpService;

        public LoginViewModel(INavigationService navigationService,IHttpService httpService)
        {
            _navigationService = navigationService;
            _httpService = httpService;
        }
        public string Password { get; set; }

        public string UserName { get; set; }

        public bool CanLogin(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        public async Task Login()
        {
             var token = await _httpService.LogIn(new User
            {
                username = UserName,
                password = Password,
            });
            
            if (!string.IsNullOrEmpty(token.Token))
            {
                _navigationService.NavigateToViewModel<ServerListViewModel>();
            }
        }

        public void OnPasswordChanged(WatermarkPasswordBox source)
        {
            Password = source.Password;
        }

    }
}
