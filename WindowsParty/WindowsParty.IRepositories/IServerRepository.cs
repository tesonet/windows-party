using System.Collections.Generic;
using WindowsParty.Models;

namespace WindowsParty.IRepositories
{
    public  interface IServerRepository
    {
        IEnumerable<Server> GetServers(string accessToken);
    }
}
