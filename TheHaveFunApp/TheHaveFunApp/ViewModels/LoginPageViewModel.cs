using CommonServiceLocator;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHaveFunApp.Services;
using TheHaveFunApp.Views;

namespace TheHaveFunApp.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly IHttpService _httpService;
        private readonly IRegionManager _regionManager;
        private string _password;
        private string _userName;
        public LoginPageViewModel(IRegionManager regionManager, IHttpService httpService)
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);
            _regionManager = regionManager;
            _httpService = httpService;
        }
        public DelegateCommand LoginCommand { get; }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanLogin()
        {
            return true;
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        private async void Login()
        {
            await Task.Delay(20);
            Console.WriteLine("Login");

            if (_httpService.Login("", ""))
            {
                _regionManager.RequestNavigate("MainRegion", "ServersListPage");
            }
        }
    }
}
