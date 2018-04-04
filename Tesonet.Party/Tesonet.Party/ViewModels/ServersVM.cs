using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tesonet.Party.Contracts;
using Tesonet.Party.Services;
using Tesonet.Party.Utils;
using Unity;

namespace Tesonet.Party.ViewModels
{
    public class ServersVM : ViewModelBase
    {
        public ServersVM()
        {

        }

        public ServersVM(IUnityContainer container) : base(container)
        {
            LogoutCommand = new DelegateCommand(Logout);
            Initialize();
        }

        private void Logout()
        {
            container.Resolve<ISessionService>().Logout();
        }

        public async void Initialize()
        {
            IsBusy = true;
            var token = container.Resolve<ISessionService>().Token;
            var result = await container.Resolve<ITesonetServiceAgent>().GetServers(token);
            if (string.IsNullOrEmpty(result.Message))
            {
                foreach (var server in result.Servers)
                    Servers.Add(server);
            }
            else
                ErrorMessage = result.Message;
            IsBusy = false;
        }

        public ObservableCollection<Server> Servers { get; } = new ObservableCollection<Server>();

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
                }
            }
        }

        private ICommand _LogoutCommand;
        public ICommand LogoutCommand
        {
            get { return _LogoutCommand; }
            set
            {
                if (_LogoutCommand != value)
                {
                    _LogoutCommand = value;
                    OnPropertyChanged(nameof(LogoutCommand));
                }
            }
        }

    }
}
