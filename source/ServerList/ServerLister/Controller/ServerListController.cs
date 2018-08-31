using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ServerLister.Service.Interfaces;
using ServerLister.Service.Interfaces.Dto;
using ServerListerApp.Annotations;
using ServerListerApp.Interfaces;
using ServiceLister.Common.Implementation;

namespace ServerListerApp.Controller
{
    public class ServerListController : IServerListController
    {
        private List<ServerDto> _serverList;

        public ServerListController(IServerListerService serverListerService)
        {
            ServerList = serverListerService.GetServers();
        }

        public List<ServerDto> ServerList
        {
            get => _serverList;
            set
            {
                _serverList = value;
                OnPropertyChanged();
            }
        }

        public void LogOut()
        {
            Authorization.Instance.DestroyToken();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}