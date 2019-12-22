
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WindowsParty.Events;
using WindowsParty.Interfaces;
using WindowsParty.Models;

namespace WindowsParty.ViewModels
{
    public class ServerListViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IWebTasks _webTasks;
        private IAuthenticationHelper _authenticationHelper;
        private ObservableCollection<ServerModel> _serverList;

        public ServerListViewModel(IEventAggregator eventAggregator, IAuthenticationHelper authenticationHelper, IWebTasks webTasks)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _authenticationHelper = authenticationHelper ?? throw new ArgumentNullException(nameof(authenticationHelper));
            _webTasks = webTasks ?? throw new ArgumentNullException(nameof(webTasks));
            _serverList = new ObservableCollection<ServerModel>();
        }

        protected async override void OnActivate()
        {
            base.OnActivate();

            //On activation - call RetrieveServerList method
            await RetrieveServerList();
        }

        internal void AddServers(List<ServerModel> serverList)
        {
            foreach(var server in serverList)
            {
                Servers.Add(server);
            }
        }

        public ObservableCollection<ServerModel> Servers
        {
            get => _serverList;
            set
            {
                _serverList = value;
            }
        }

        public async Task RetrieveServerList()
        {
            try
            {
                List<ServerModel> serverList = await _webTasks.RetrieveServerList(_authenticationHelper.AuthModel);

                //Check if returned server list has any servers
                if (serverList != null && serverList.Count > 0)
                {
                    AddServers(serverList);
                }
                else
                {
                    throw new Exception("Failed to retrieve a list of servers");
                }
            }
            catch (TaskCanceledException ex)
            {
                LogManager.GetLog(this.GetType()).Info("Call to retrieve servers was cancelled: {0}", ex);
                throw new Exception("Failed retrieving server list");
            }
        }

        public void LogOut()
        {
            _eventAggregator.PublishOnUIThread(new EventModel(Status.Logout));
        }
    }
}
