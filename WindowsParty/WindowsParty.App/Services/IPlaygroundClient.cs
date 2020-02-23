using System.Threading.Tasks;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.Services
{
    public interface IPlaygroundClient
    {
        Task<GetTokenResponse> GetToken(string userName, string password);

        Task<GetServersResponse[]> GetServers(string token);
    }
}
