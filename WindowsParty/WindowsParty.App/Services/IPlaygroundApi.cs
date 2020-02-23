using Refit;
using System.Threading.Tasks;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.Services
{
    public interface IPlaygroundApi
    {
        [Post("/v1/tokens")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<GetTokenResponse> GetToken([Body]GetTokenRequest tokenRequest);

        [Get("/v1/servers")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<GetServersResponse[]> GetServers([Header("Authorization")] string autorization);
    }
}
