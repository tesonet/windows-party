using System.Collections.Generic;

namespace WindowsParty.Core.Responses
{
    public class ServerListResponse
    {
        public ICollection<ServerInfo> Servers { get; private set; }

        public ServerListResponse(ICollection<ServerInfo> servers)
        {
            Servers = servers;
        }
    }

    public class ServerInfo
    {
        public string Name { get; set; }
        public int Distance { get; set; }
    }
}
