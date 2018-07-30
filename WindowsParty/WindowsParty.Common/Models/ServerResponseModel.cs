
using Newtonsoft.Json;

namespace WindowsParty.Common.Models
{
    public class ServerResponseModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "distance")]
        public int Distance { get; set; }
    }
}
