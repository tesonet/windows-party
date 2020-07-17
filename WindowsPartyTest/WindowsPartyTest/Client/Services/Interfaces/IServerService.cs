using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Models;

namespace WindowsPartyTest.Client.Services.Interfaces
{
    public interface IServerService
    {
        List<ServerModel> LoadServers();
    }
}
