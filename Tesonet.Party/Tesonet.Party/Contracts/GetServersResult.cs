using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet.Party.Contracts
{
    public class GetServersResult : ActionResult
    {
        public List<Server> Servers { get; set; }
    }
}
