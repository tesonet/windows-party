using Caliburn.Micro;
using WindowsParty.App.Interfaces;
using WindowsParty.App.Models.V1;

namespace WindowsParty.App.ViewModels
{
    public class DashboardViewModel : Screen
    {
        private readonly IServersDataService _dataService;

        public DashboardViewModel(IServersDataService dataService)
        {
            _dataService = dataService;
        }


        protected override async void OnInitialize()
        {
            // TODO: loader?
            var servers = await _dataService.GetServers();

            Servers = new BindableCollection<Server>(servers);

            base.OnInitialize();
        }

        public BindableCollection<Server> Servers { get; private set; }
    }
}
