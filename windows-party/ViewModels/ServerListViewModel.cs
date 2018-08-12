using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Tesonet.Windows.Party.Helpers;
using Tesonet.Windows.Party.Models;
using Tesonet.Windows.Party.Repositories;

namespace Tesonet.Windows.Party.ViewModels
{
    public class ServerListViewModel : ViewModelBase
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public User LoggedInUser { get; set; }

        private readonly IServerRepository _serverRepository;

        public Action LogOut = delegate { };

        public RelayCommand LogOutCommand { get; private set; }

        private ObservableCollection<Server> _servers;
        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => SetProperty(ref _servers, value);
        }

        public ServerListViewModel(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
            LogOutCommand = new RelayCommand(OnLogOut);
        }

        public async Task LoadServers()
        {
            try
            {
                Servers = new ObservableCollection<Server>(await _serverRepository.GetServers(LoggedInUser.Token));
            }
            catch (Exception ex)
            {
                _log.Error("Error retrieving server list.", ex);
            }
        }

        private void OnLogOut()
        {
            LoggedInUser = null;
            LogOut();
        }
    }
}
