using ServerLocator.Client.Shared;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ServerLocator.Client.ServerClient
{
    public interface IServerClient
    {
        Task<bool> TryAuthenticateAsync(NetworkCredential credentials);
        Task<IEnumerable<ServerModel>> GetServersAsync();
    }
}
