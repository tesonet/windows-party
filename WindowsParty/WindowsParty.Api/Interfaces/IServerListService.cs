using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;

namespace WindowsParty.Api
{
    public interface IServerListService
    {
        Task<List<Server>> GetServerList();
    }
}
