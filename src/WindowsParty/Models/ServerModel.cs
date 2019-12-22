using Newtonsoft.Json;

namespace WindowsParty.Models
{
    public class ServerModel
    {
        [JsonProperty("name")]
        public string ServerName { get; set; }

        [JsonProperty("distance")]
        public decimal Distance { get; set; }
    }
}
