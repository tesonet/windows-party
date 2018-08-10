using System.Collections.Generic;
using WindowsParty.Models;

namespace WindowsParty.IServices
{
    public interface IServerService
    {
        IEnumerable<Server> GetServers(string accessToken);
    }
}