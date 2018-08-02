using Newtonsoft.Json;

namespace tesonet.windowsparty.contracts
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }
    }
}
