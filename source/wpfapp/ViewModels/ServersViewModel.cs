using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.services.Navigation;
using tesonet.windowsparty.services.Servers;
using tesonet.windowsparty.wpfapp.Commands;
using tesonet.windowsparty.wpfapp.Navigation;

namespace tesonet.windowsparty.wpfapp.ViewModels
{
    public class ServersViewModel : BaseNotification, IServersViewModel
    {
        private readonly INavigator _navigator;
        private readonly IServersService _serversService;
        private string _error;

        public ServersViewModel(INavigator navigator, IServersService serversService)
        {
            _serversService = serversService;
            _navigator = navigator;
            _navigator.SubscribeToNavigationItem<IServersViewModel, IToServersView>(this, OnNavigationToServersView);

            Servers = new ObservableCollection<Server>();
            LogoutCommand = new Command(CanLogoutAction, LogoutAction);
        }

        public ObservableCollection<Server> Servers { get; private set; }

        public ICommand LogoutCommand { get; private set; }

        public bool CanLogout { get { return true; } }

        public string ErrorMessage
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        private bool CanLogoutAction()
        {
            return CanLogout;
        }

        private void LogoutAction()
        {
            _navigator.PublishNavigationItem(new FromServersView());
        }

        private async void OnNavigationToServersView(IToServersView toServersView)
        {
            Servers.Clear();
            await OnNavigationToServersViewAsync(toServersView);
        }

        private async Task OnNavigationToServersViewAsync(IToServersView toServersView)
        {
            try
            {
                var servers = await _serversService.Get(toServersView.Token);
                OnServers(servers);
            }
            catch(ServersServiceException e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void OnServers(Server[] servers)
        {
            foreach (var s in servers)
            {
                Servers.Add(s);
            }
        }
    }
}
