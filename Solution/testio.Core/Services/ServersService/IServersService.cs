using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testio.Core.Services.ServersService
{
    public interface IServersService
    {
        Task<IEnumerable<Server>> GetServerList(string token);
    }
}
