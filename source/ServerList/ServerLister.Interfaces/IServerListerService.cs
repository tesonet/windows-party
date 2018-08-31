using System.Collections.Generic;
using ServerLister.Service.Interfaces.Dto;

namespace ServerLister.Service.Interfaces
{
    public interface IServerListerService
    {
        List<ServerDto> GetServers();
    }
}