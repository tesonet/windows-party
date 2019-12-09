using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windowsparty.Model;
using WindowsParty.Api;
using WindowsParty.Data.Repositories;

namespace WindowsParty.UI.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IMainViewModel
    {
        private readonly IServerListService _serverListService;
        private List<Server> _serverList;

        public List<Server> ServerList
        {
            get { return _serverList; }
            set { _serverList = value; }
        }

        public MainViewModel(IServerListService serverListService)
        {
            _serverListService = serverListService ?? throw new ArgumentNullException(nameof(serverListService));
            Task.Run(async () =>
            {
                ServerList = await _serverListService.GetServerList();
            });
        }
    }
}
