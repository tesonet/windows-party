using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsPartyApp.Model;

namespace WindowsPartyApp.Api
{
    public interface IApi
    {
        Task<AuthToken> Login(Credentials credentials);

        Task<IEnumerable<Server>> GetServers(string authToken);
    }
}
