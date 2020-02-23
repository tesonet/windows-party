using System.Threading.Tasks;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.Services
{
    public class PlaygroundClient : IPlaygroundClient
    {
        private readonly IPlaygroundApi _playgroundApi;

        public PlaygroundClient(IPlaygroundApi playgroundApi)
        {
            _playgroundApi = playgroundApi;
        }

        public Task<GetServersResponse[]> GetServers(string token)
        {
            return _playgroundApi.GetServers($"Bearer {token}");
        }

        public Task<GetTokenResponse> GetToken(string userName, string password)
        {
            return _playgroundApi.GetToken(new GetTokenRequest
            {
                Username = userName,
                Password = password
            });
        }
    }
}
