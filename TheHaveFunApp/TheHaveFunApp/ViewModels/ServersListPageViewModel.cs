using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHaveFunApp.Models;
using TheHaveFunApp.Services;

namespace TheHaveFunApp.ViewModels
{
    public class ServersListPageViewModel : BindableBase
    {

        private readonly IHttpService _httpService;

        private readonly IRegionManager _regionManager;

        public ServersListPageViewModel(IRegionManager regionManager, IHttpService httpService)
        {
            _regionManager = regionManager;
            _httpService = httpService;

            FetchServers();
        }

        private void FetchServers()
        {
            Servers = new List<ServerModel>(_httpService.GetServersList());
        }


        public List<ServerModel> Servers { get; private set; } = new List<ServerModel>();
    }
}
