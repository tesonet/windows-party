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
        string Username { get; set; }
        string Password { get; set; }
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
            var agent = container.Resolve<ITesonetServiceAgent>();
            var token = await agent.Login(Username, Password);

            Console.WriteLine($"Success {token}");

            var servers = await agent.GetServers(token.Token);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
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
