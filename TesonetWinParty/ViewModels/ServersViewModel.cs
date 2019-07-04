using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesonetWinParty.EventModels;
using TesonetWinParty.Helpers;
using TesonetWinParty.Models;

namespace TesonetWinParty.ViewModels
{
    public class ServersViewModel : Screen
    {
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private IAccountHelper _accountHelper;
        private BindingList<Server> _servers;

        public ServersViewModel(IAPIHelper apiHelper, IEventAggregator events, IAccountHelper accountHelper)
        {
            _events = events;
            _apiHelper = apiHelper;
            _accountHelper = accountHelper;
        }

        public BindingList<Server> Servers
        {
            get { return _servers; }
            set
            {
                _servers = value;
                NotifyOfPropertyChange(() => Servers);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadServers();
        }

        public async Task LoadServers()
        {
            var serversList = await _apiHelper.GetServersList(_accountHelper.Token.Token);
            Servers = new BindingList<Server>(serversList);
        }

        public void LogOut()
        {
            _events.PublishOnUIThread(new LogOutEvent());
        }
    }
}
