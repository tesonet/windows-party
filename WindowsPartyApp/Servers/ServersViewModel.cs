using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsPartyApp.Api;
using WindowsPartyApp.Model;

namespace WindowsPartyApp.Servers
{
    public class ServersViewModel: Screen
    {
        private readonly IEventAggregator eventAggregator;
        private readonly AuthToken authToken;
        private IApi tesonetApi;
        private IEnumerable<Server> servers;

        public IEnumerable<Server> Servers
        {
            get { return servers; }
            set
            {
                servers = value;
                NotifyOfPropertyChange(() => servers);
            }
        }

        public ServersViewModel(AuthToken authToken, IApi tesonetApi, IEventAggregator eventAggregator)
        {
            this.tesonetApi = tesonetApi;
            this.eventAggregator = eventAggregator;
            this.authToken = authToken;
        }

        public async void Logout()
        {
            await eventAggregator.PublishOnUIThreadAsync(new LogoutMessage());
        }

        protected override async void OnActivate()
        {
            base.OnActivate();

            var items = await GetServers();

            Servers = items.Select(i => new Server { Name = i.Name, Distance = i.Distance + " km" });
        }

        private async Task<IEnumerable<Server>> GetServers()
        {
            return await tesonetApi.GetServers(authToken.Token);
        }
    }
}
