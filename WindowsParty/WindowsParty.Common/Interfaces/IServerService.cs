
using System.Threading.Tasks;
using WindowsParty.Common.Models;

namespace WindowsParty.Common.Interfaces
{
    /// <summary>
    /// Service manages Servers
    /// </summary>
    public interface IServerService
    {
        Task<ServerResultModel> GetServers();
    }
}
