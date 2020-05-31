using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesonetTestTask.Model
{
    [JsonObject]
    public class ServerModel
    {
        [JsonProperty(PropertyName = "name")]
        public string name { set; get; }
        [JsonProperty(PropertyName = "distance")]
        public string distance { set; get; }
    }
}
