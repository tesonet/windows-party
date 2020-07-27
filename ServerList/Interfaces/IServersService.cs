using System.Net.Http;
using System.Threading.Tasks;

namespace ServerList.Interfaces
{
    public interface IServersService
    {
        Task<HttpResponseMessage> GetServersListAsync(string authorizationToken);
    }
}
