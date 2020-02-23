using WindowsParty.App.Services.Models;

namespace WindowsParty.App.Domain.Events
{
    public class ServerListRetrievedEvent
    {
        public GetServersResponse[] Servers { get; set; }
    }
}
