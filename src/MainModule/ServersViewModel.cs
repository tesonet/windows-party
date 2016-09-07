using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using WindowsParty.Infrastructure;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Domain;
using WindowsParty.Infrastructure.Navigation;
using Prism.Commands;
using Prism.Regions;
using RestSharp;

namespace MainModule
{
    public class ServersViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly INavigator _navigator;
        private readonly IServerListProvider _serverListProvider;
        private IEnumerable<Server> _servers;
        private RestRequestAsyncHandle _serverUpdateActionHandle;
        public ICommand LogoutCommand { get; }

        public IEnumerable<Server> Servers   
        {
            get { return _servers; }
            set
            {
                _servers = value; 
                OnPropertyChanged();
            }
        }

        public ServersViewModel(INavigator navigator, IServerListProvider serverListProvider)
        {
            _navigator = navigator;
            _serverListProvider = serverListProvider;
            LogoutCommand = new DelegateCommand(OnLogout);

            Servers = new List<Server>
            {
                new Server {Name = "Alzyras",Distance = "Toli" },
                new Server {Name = "Vilnius",Distance = "1" },
            };
        }

        private void OnLogout()
        {
            _navigator.GoTo(AppViews.InitialView);
        }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
           _serverUpdateActionHandle = _serverListProvider.GetServersAsync((string)navigationContext.Parameters["token"], UpdateServersWithResponseData);
        }

        private void UpdateServersWithResponseData(List<Server> servers)
        {
            Thread.Sleep(2000);
            Servers = servers.Select(s => new Server { Distance = $"{s.Distance} km", Name = s.Name }); ;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _serverUpdateActionHandle?.Abort();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
