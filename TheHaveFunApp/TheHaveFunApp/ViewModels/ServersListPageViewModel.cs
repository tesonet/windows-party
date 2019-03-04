using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using TheHaveFunApp.Enums;
using TheHaveFunApp.Models;
using TheHaveFunApp.Services;

namespace TheHaveFunApp.ViewModels
{
    public class ServersListPageViewModel : BindableBase
    {

        private readonly IHttpService _httpService;

        private readonly IRegionManager _regionManager;

        private SortType _sortedByDistance = SortType.Default;

        private SortType _sortedByName = SortType.Default;

        public ServersListPageViewModel(IRegionManager regionManager, IHttpService httpService)
        {
            _regionManager = regionManager;
            _httpService = httpService;

            FetchServers();

            LogoutCommand = new DelegateCommand(Logout);
            SortCommand = new DelegateCommand<string>(Sort);
        }

        public DelegateCommand LogoutCommand { get; }

        public List<ServerModel> Servers { get; private set; } = new List<ServerModel>();
        public DelegateCommand<string> SortCommand { get; }
        private void FetchServers()
        {
            Servers = new List<ServerModel>(_httpService.GetServersList());
        }

        private void Logout()
        {
            _httpService.Logout();
            _regionManager.RequestNavigate("MainRegion", "LoginPage");
        }

        private void Sort(string column)
        {
            if (column == "name")
            {
                if (_sortedByName == SortType.Default || _sortedByName == SortType.Desc)
                {
                    Servers = new List<ServerModel>(Servers.OrderBy(i => i.Name));
                    _sortedByName = SortType.Asc;
                }
                else
                {
                    Servers = new List<ServerModel>(Servers.OrderByDescending(i => i.Name));
                    _sortedByName = SortType.Desc;
                }
            }
            else
            {
                if (_sortedByDistance == SortType.Default || _sortedByDistance == SortType.Desc)
                {
                    Servers = new List<ServerModel>(Servers.OrderBy(i => i.Distance));
                    _sortedByDistance = SortType.Asc;
                }
                else
                {
                    Servers = new List<ServerModel>(Servers.OrderByDescending(i => i.Distance));
                    _sortedByDistance = SortType.Desc;
                }
            }
            this.RaisePropertyChanged(nameof(Servers));
        }
    }
}
