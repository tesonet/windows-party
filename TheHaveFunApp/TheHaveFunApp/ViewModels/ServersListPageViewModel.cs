using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheHaveFunApp.Collections;
using TheHaveFunApp.Enums;
using TheHaveFunApp.Models;
using TheHaveFunApp.Services;

namespace TheHaveFunApp.ViewModels
{
    public class ServersListPageViewModel : BindableBase, INavigationAware
    {

        private readonly IHttpService _httpService;

        private readonly IRegionManager _regionManager;



        public ServersListPageViewModel(IRegionManager regionManager, IHttpService httpService)
        {
            _regionManager = regionManager;
            _httpService = httpService;



            LogoutCommand = new DelegateCommand(Logout);
            SortCommand = new DelegateCommand<string>(Sort);
        }

        public DelegateCommand LogoutCommand { get; }

        public ServersList Servers { get; private set; } = new ServersList();
        public DelegateCommand<string> SortCommand { get; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            FetchServers();
        }

        private async void FetchServers()
        {
            await Task.Delay(500);
            Servers.Clear();
            Servers.AddRange(_httpService.GetServersList());
        }



        private void Logout()
        {
            _httpService.Logout();
            _regionManager.RequestNavigate("MainRegion", "LoginPage");
        }

        private void Sort(string column)
        {
            Servers.SortByProperty(column);

            this.RaisePropertyChanged(nameof(Servers));
        }
    }
}
