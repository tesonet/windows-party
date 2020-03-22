using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.App.Models.V1;

namespace WindowsParty.App.Interfaces
{
    public interface IServersDataService
    {
        Task<IEnumerable<Server>> GetServers();
    }
}
