using Newtonsoft.Json;

namespace WindowsParty.ApiServices.Models
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distance")]
        public long Distance { get; set; }
    }
}
