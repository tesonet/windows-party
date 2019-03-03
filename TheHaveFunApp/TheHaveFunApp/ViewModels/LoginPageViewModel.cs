using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHaveFunApp.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private string _password;
        private string _userName;
        public LoginPageViewModel()
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);
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

        private async void Login()
        {
            await Task.Delay(500);
            Console.WriteLine("Login");

        }
    }
}
