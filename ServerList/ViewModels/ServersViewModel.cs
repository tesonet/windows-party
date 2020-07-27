using Caliburn.Micro;
using ServerList.Messages;
using ServerList.Models;
using System.Collections.Generic;

namespace ServerList.ViewModels
{
    public class ServersViewModel : Screen, IHandle<ServerListMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private List<Server> _serverList;
        private readonly ILog _logger;

        public ServersViewModel(IEventAggregator eventAggregator, ILog logger)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _logger = logger;
        }

        public List<Server> ServerList
        {
            get { return _serverList; }
            set { _serverList = value; }
        }

        public void Logout()
        {
            _logger.Info($"Logging out.");
            _eventAggregator.PublishOnUIThread(new NavigationMessage(PageName.Login));
        }

        public void Handle(ServerListMessage message)
        {
            ServerList = message.serverList;
            _logger.Info($"Received {_serverList.Count} servers in the list.");
        }

        ~ServersViewModel()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}
