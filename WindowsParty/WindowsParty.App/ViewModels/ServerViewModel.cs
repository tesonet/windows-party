using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WindowsParty.App.Domain;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Events;
using WindowsParty.App.Models;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.ViewModels
{
    public class ServerViewModel : Conductor<object>.Collection.OneActive,
        IHandle<ServerListRetrievedEvent>,
        IHandleWithTask<FailedToRetrieveServersEvent>
    {
        private ObservableCollection<ServerModel> _serverList;

        private readonly IEventAggregator _eventAggregator;
        private readonly ICommandProcessor _commandProcessor;
        private readonly ITokenService _tokenService;

        public ServerViewModel(IEventAggregator eventAggregator, ICommandProcessor commandProcessor, ITokenService tokenService)
        {
            if(tokenService.Token == null)
            {
                throw new ArgumentNullException(nameof(tokenService.Token));
            }

            _tokenService = tokenService;

            _serverList = new ObservableCollection<ServerModel>();

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _commandProcessor = commandProcessor;
        }

        public ObservableCollection<ServerModel> ServerList
        {
            get { return _serverList; }
            set 
            { 
                _serverList = value;
                NotifyOfPropertyChange(() => ServerList);
            }
        }

        public void Logout()
        {
            var conductor = Parent as IConductor;
            conductor.ActivateItem(IoC.Get<LoginViewModel>());
        }

        protected override void OnActivate()
        {
            _commandProcessor.ProcessAsync(new GetServersCommand(_tokenService.Token));
        }

        public async Task Handle(FailedToRetrieveServersEvent message)
        {
            await DialogHost.Show(new { ErrorMessage = "Failed to retrieve server list" });       
        }

        public void Handle(ServerListRetrievedEvent message)
        {
            foreach (var server in message.Servers)
            {
                ServerList.Add(new ServerModel
                {
                    Server = server.Name,
                    Distance = server.Distance
                });
            }
        }
    }
}
