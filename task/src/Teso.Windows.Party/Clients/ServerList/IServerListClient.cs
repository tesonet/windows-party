using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Teso.Windows.Party.Models;

namespace Teso.Windows.Party.Clients.ServerList
{
    public interface IServerListClient
    {
        Task<IEnumerable<Server>> GetServerList(string token, CancellationToken cancellationToken);
    }
}