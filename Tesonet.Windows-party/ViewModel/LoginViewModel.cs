using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Tesonet.Windows_party.Models;
using Tesonet.Windows_party.Services.Interfaces;

namespace Tesonet.Windows_party.ViewModel
{  
    public class LoginViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private bool _credentialsErrorVisible;
        private bool _isSpinnerVisible;        

        public LoginViewModel(IApiService apiService, INavigationService navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public string UserName { get; set; }        

        public ICommand LoginClickCommand => new RelayCommand<PasswordBox>(LoginClick);

        public bool CredentialsErrorVisible
        {
            get { return _credentialsErrorVisible; }
            set
            {
                _credentialsErrorVisible = value;
                RaisePropertyChanged(() => CredentialsErrorVisible);
            }
        }

        public bool IsSpinnerVisible
        {
            get { return _isSpinnerVisible; }
            set
            {
                _isSpinnerVisible = value;
                RaisePropertyChanged(() => IsSpinnerVisible);
            }
        }

        private async void LoginClick(PasswordBox passwordBox)
        {
            IsSpinnerVisible = true;
            var result = await _apiService.Login(new LoginModel
            {
                UserName = UserName,
                Password = passwordBox.Password
            });
            IsSpinnerVisible = false;

            if (result)
            {
                _navigationService.ShowServerListView();
            }
            else
            {
                CredentialsErrorVisible = true;
            }
        }
    }
}