using API;
using API.Communicator.Models;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UI.Event;
using UI.Models;

namespace UI.ViewModels
{
    public class ServersViewModel : Screen
    {
        IEventAggregator events;
        private BindingList<ServerInfo> serversList;

        public BindingList<ServerInfo> ServersList
        {
            get { return serversList; }
            set
            {
                serversList = value;
                NotifyOfPropertyChange(() => ServersList);
            }
        }

        public ServersViewModel(IEventAggregator events)
        {
            this.events = events;
        }

        public void LogOut()
        {
            events.BeginPublishOnUIThread(EventsEnum.LogOut);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await GetServersList();
        }

        private async Task GetServersList()
        {
            List<ServerInfoModel> servers = await Program.Communicator.GetServerList();
            ServersList = new BindingList<ServerInfo>(GenerateServersDistance(servers));
        }

        private List<ServerInfo> GenerateServersDistance(List<ServerInfoModel> servers)
        {
            List<ServerInfo> mappedServers = new List<ServerInfo>();
            servers.ForEach(server =>
            {
                mappedServers.Add(new ServerInfo(server.Name, server.Distance + " km"));
            });
            return mappedServers;
        }
    }
}
