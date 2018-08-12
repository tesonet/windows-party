using Newtonsoft.Json;

namespace Tesonet.ServerProxy.Dto
{
    [JsonObject]
    public class ServerDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("distance")]
        public string Distance { get; set; }
    }
}