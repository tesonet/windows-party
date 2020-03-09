using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Teso.Windows.Party.Clients.ServerList;
using Teso.Windows.Party.Events;
using Teso.Windows.Party.Models;

namespace Teso.Windows.Party.ServerList
{
    public class ServerListViewModel : Screen
    {
        public ObservableCollection<Server> Servers { get; set; } = new ObservableCollection<Server>();

        private readonly IEventAggregator _eventAggregator;
        private readonly IServerListClient _serverListClient;
        private readonly User _user;
        private ILog _logger;
        private CancellationTokenSource _cancellationTokenSource;

        public ServerListViewModel(IEventAggregator eventAggregator, IServerListClient serverListClient, User user)
        {
            _eventAggregator = eventAggregator;
            _serverListClient = serverListClient;
            _user = user;
            _logger = LogManager.GetLog(GetType());
        }

        protected override async void OnActivate()
        {
            await GetServerList();
            base.OnActivate();
        }

        private async Task GetServerList()
        {
            Servers.Clear();

            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                IEnumerable<Server> servers =
                    await _serverListClient.GetServerList(_user.Token, _cancellationTokenSource.Token);

                foreach (Server server in servers)
                {
                    Servers.Add(server);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception);

                Xceed.Wpf.Toolkit.MessageBox.Show("Failed to get server list!", String.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                //TODO: implement retry with F5 hit?
                Logout();
            }
            finally
            {
                _cancellationTokenSource = null;
            }

            _logger.Info("Got server list");
        }

        public void Logout()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }

            _eventAggregator.PublishOnUIThread(new ChangeEvent(ChangeAction.LoggedOut));

            _logger.Info("Logged out");
        }
    }
}