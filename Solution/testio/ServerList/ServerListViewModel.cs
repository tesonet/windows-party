using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using testio.Core.Services.ServersService;
using testio.Core.Services.AuthenticationService;
using testio.HandleMessages.Navigation;
using System.Collections.ObjectModel;
using testio.Caliburn;
using testio.Core.Logging;
using System.ComponentModel;

namespace testio.ServerList
{
    public class ServerListViewModel : BaseScreen
    {
        #region Fields

        private IAuthenticationService _authenticationService = null;
        private IServersService _serversService = null;
        private IEventAggregator _eventAggregator = null;

        #endregion Fields

        #region Constructors        

        public ServerListViewModel(IAuthenticationService authenticationService, IServersService serversService, IEventAggregator eventAggregator, ILogger logger):
            base(logger)
        {
            _authenticationService = authenticationService;
            _serversService = serversService;
            _eventAggregator = eventAggregator;

            Servers = new ObservableCollection<Server>();
        }

        #endregion Constructors

        protected override void OnActivate()
        {        
            base.OnActivate();
            LoadServerList(true);
        }

        private async void LoadServerList(bool onUIThread)
        {
            try
            {
                SetBusy();

                var servers = await _serversService.GetServerList(_authenticationService.Token);

                foreach (var server in servers)
                {
                    Servers.Add(server);
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
            finally
            {
                OnUIThread(() =>
                {
                    SetNotBusy();
                });
            }
        }

        public void Logout()
        {
            _authenticationService.Logout();
            _eventAggregator.PublishOnUIThread(new NavigationMessage(TargetWindow.Login));
        }

        #region Properties

        public ObservableCollection<Server> Servers
        {
            get;
            set;
        }

        #endregion Properties
    }
}
