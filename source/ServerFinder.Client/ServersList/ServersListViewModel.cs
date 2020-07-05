namespace ServerFinder.Client.ServersList
{
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using Integration;
    using ServerFinder.Client.ApplicationShell;

    public class ServersListViewModel : Screen
    {
        private readonly IServiceClient _serverClient;
        private readonly IEventAggregator _eventAggregator;

        private BindingList<ServerInfo> serversList;

        public BindingList<ServerInfo> ServersList
        {
            get => serversList;
            set
            {
                serversList = value;
                NotifyOfPropertyChange(() => ServersList);
            }
        }

        public ServersListViewModel(IServiceClient serverClient, IEventAggregator eventAggregator)
        {
            _serverClient = serverClient;
            _eventAggregator = eventAggregator;
        }

        public void LogOut()
        {
            _serverClient.LogOut();
            _eventAggregator.PublishOnUIThread(new ApplicationEvent(ApplicationEventType.LogOut));
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await InitServersList(CancellationToken.None);
        }

        private async Task InitServersList(CancellationToken cancellationToken)
        {
            var servers = await _serverClient.GetServerList(cancellationToken);
            ServersList = new BindingList<ServerInfo>(servers
                .Select(s => new ServerInfo(s.Name, s.Distance + " km"))
                .ToList());
        }
    }
}
