using System.Threading.Tasks;
using WindowsParty.Core.External;
using WindowsParty.Core.External.PlaygroundClient;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;

namespace WindowsParty.Core.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(TokenRequest tokenRequest);
    }

    public class TokenService : ITokenService
    {
        private readonly IPlaygroundClient _playgroundClient;

        public TokenService(IPlaygroundClient playgroundClient)
        {
            _playgroundClient = playgroundClient;
        }

        public async Task<TokenResponse> GetToken(TokenRequest tokenRequest)
        {
            return await _playgroundClient.GetToken(tokenRequest);
        }
    }
}
