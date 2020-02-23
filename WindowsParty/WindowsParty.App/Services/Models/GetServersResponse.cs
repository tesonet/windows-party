using Newtonsoft.Json;

namespace WindowsParty.App.Services.Models
{
    public class GetServersResponse
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public int Distance { get; set; }

    }
}
