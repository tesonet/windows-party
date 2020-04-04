using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPFApp.Interfaces;
using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class ServerListViewModel: Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IHttpService _httpService;

        public ObservableCollection<ServerModel> Server { get; set; } = new ObservableCollection<ServerModel>();
        public ServerListViewModel( INavigationService navigationService, IHttpService httpService)
        {
            _navigationService = navigationService;
            _httpService = httpService;
        }

        protected override async void OnActivate()
        {
            await GetServersList();
        }

        private async Task GetServersList()
        {
            Server.Clear();

            IEnumerable<ServerModel> serversList = await _httpService.GetServerList();

            foreach (ServerModel serverItem in serversList)
            {
                Server.Add(serverItem);
            }
        }
        public void LogOut()
        {
            _navigationService.NavigateToViewModel<LoginViewModel>();
        }

    }
}
