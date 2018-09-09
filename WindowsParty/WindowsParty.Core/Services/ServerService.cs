using System.Threading.Tasks;
using WindowsParty.Core.External;
using WindowsParty.Core.External.PlaygroundClient;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;

namespace WindowsParty.Core.Services
{
    public interface IServerService
    {
        Task<ServerListResponse> GetServerList(ServerListRequest serverListRequest);
    }

    public class ServerService : IServerService
    {
        private readonly IPlaygroundClient _playgroundClient;

        public ServerService(IPlaygroundClient playgroundClient)
        {
            _playgroundClient = playgroundClient;
        }

        public async Task<ServerListResponse> GetServerList(ServerListRequest serverListRequest)
        {
            return await _playgroundClient.GetServerList(serverListRequest);
        }
    }
}
