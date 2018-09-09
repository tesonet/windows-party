using System.Threading.Tasks;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;

namespace WindowsParty.Core.External.PlaygroundClient
{
    public interface IPlaygroundClient
    {
        Task<TokenResponse> GetToken(TokenRequest tokenRequest);
        Task<ServerListResponse> GetServerList(ServerListRequest serverListRequest);
    }
}
