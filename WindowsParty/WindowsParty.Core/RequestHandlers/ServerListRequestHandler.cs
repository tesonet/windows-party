using System.Threading;
using System.Threading.Tasks;
using WindowsParty.Core.External;
using WindowsParty.Core.External.PlaygroundClient;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;
using MediatR;

namespace WindowsParty.Core.RequestHandlers
{
    public class ServerListRequestHandler : IRequestHandler<ServerListRequest, ServerListResponse>
    {
        private readonly IPlaygroundClient _playgroundClient;

        public ServerListRequestHandler(IPlaygroundClient playgroundClient)
        {
            _playgroundClient = playgroundClient;
        }

        public async Task<ServerListResponse> Handle(ServerListRequest request, CancellationToken cancellationToken)
        {
            return await _playgroundClient.GetServerList(request);
        }
    }
}
