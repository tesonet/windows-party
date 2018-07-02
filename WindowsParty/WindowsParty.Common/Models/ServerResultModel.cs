
using System.Collections.Generic;

namespace WindowsParty.Common.Models
{
    public class ServerResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<ServerResponseModel> Servers {get;set;}
    }
}
