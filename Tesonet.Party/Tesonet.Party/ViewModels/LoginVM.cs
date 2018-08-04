using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tesonet.Party.Services;
using Tesonet.Party.Utils;
using Unity;

namespace Tesonet.Party.ViewModels
{
    public interface ILoginVM
    {
        bool IsBusy { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string ErrorMessage { get; set; }
        IAsyncCommand LoginCommand { get; set; }
    }

    public class LoginVM : ViewModelBase, ILoginVM
    {
        public LoginVM()
        {
        }

        public LoginVM(IUnityContainer container) : base(container)
        {
            LoginCommand = new AwaitableDelegateCommand(Login, CanLogin);
#if DEBUG
            Username = "tesonet";
            Password = "partyanimal";
#endif
        }

        private async Task Login()
        {
            ErrorMessage = null;
            IsBusy = true;

            var result = await container.Resolve<ISessionService>().Login(Username, Password);

            IsBusy = false;
            if (!string.IsNullOrEmpty(result))
            {
                ErrorMessage = result;
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !IsBusy;
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged(nameof(Username));
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged(nameof(Password));
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (_ErrorMessage != value)
                {
                    _ErrorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        private IAsyncCommand _LoginCommand;
        public IAsyncCommand LoginCommand
        {
            get { return _LoginCommand; }
            set
            {
                if (_LoginCommand != value)
                {
                    _LoginCommand = value;
                    OnPropertyChanged(nameof(LoginCommand));
                }
            }
        }

    }
}
