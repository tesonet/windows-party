using WindowsParty.Core.Responses;
using MediatR;

namespace WindowsParty.Core.Requests
{
    public class ServerListRequest : IRequest<ServerListResponse>
    {
        public string Token { get; set; }
    }
}
