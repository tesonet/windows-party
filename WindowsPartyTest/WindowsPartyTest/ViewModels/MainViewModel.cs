using Caliburn.Micro;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.Models;
using WindowsPartyTest.ViewModels.Interfaces;

namespace WindowsPartyTest.ViewModels
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServerService _serverService;
        private IObservableCollection<ServerModel> _list;

        public MainViewModel(IEventAggregator eventAggregator, IServerService serverService)
        {
            _eventAggregator = eventAggregator;
            _serverService = serverService;
        }
        protected override void OnActivate()
        {
            base.OnActivate();
            LoadServers();
        }

        public IObservableCollection<ServerModel> Servers
        {
            get { return _list; }
            set
            {
                _list = value;
                NotifyOfPropertyChange(() => Servers);
            }
        }
        public void LoadServers()
        {
            Servers = new BindableCollection<ServerModel>(_serverService.LoadServers());
        }
    }
}
