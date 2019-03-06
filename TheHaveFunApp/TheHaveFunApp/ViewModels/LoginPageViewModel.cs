﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;
using TheHaveFunApp.Helpers;
using TheHaveFunApp.Services.Interfaces;

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
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        private bool ExecuteLogin()
        {
            return _httpService.Login(this.UserName, this.Password);
        }

        private async void Login()
        {
            using (new OverrideMouse())
            {
                if (await Task.Run(() => ExecuteLogin()))
                {
                    _regionManager.RequestNavigate("MainRegion", "ServersListPage");
                }
            }
        }
    }
}
