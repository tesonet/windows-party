using System.Collections.Generic;
using System.ComponentModel;
using ServerLister.Service.Interfaces.Dto;

namespace ServerListerApp.Interfaces
{
    public interface IServerListController : INotifyPropertyChanged
    {
        List<ServerDto> ServerList { get; set; }
        void LogOut();
    }
}