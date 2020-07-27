using System.Net.Http;
using System.Threading.Tasks;

namespace ServerList.Interfaces
{
    public interface IAuthenticationService
    {
        Task<HttpResponseMessage> LoginAsync(string username, string password);
    }
}
