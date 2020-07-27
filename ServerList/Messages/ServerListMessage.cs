using ServerList.Models;
using System.Collections.Generic;

namespace ServerList.Messages
{
    public class ServerListMessage
    {
        public List<Server> serverList;

        public ServerListMessage(List<Server> serverList)
        {
            this.serverList = serverList;
        }
    }
}
