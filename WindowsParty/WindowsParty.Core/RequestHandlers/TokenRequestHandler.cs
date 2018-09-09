using System.Threading;
using System.Threading.Tasks;
using WindowsParty.Core.External;
using WindowsParty.Core.External.PlaygroundClient;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;
using MediatR;

namespace WindowsParty.Core.RequestHandlers
{
    public class TokenRequestHandler : IRequestHandler<TokenRequest, TokenResponse>
    {
        private readonly IPlaygroundClient _playgroundClient;

        public TokenRequestHandler(IPlaygroundClient playgroundClient)
        {
            _playgroundClient = playgroundClient;
        }

        public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            return await _playgroundClient.GetToken(request);
        }
    }
}
