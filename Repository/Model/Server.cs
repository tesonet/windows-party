using Newtonsoft.Json;

namespace Repository.Model
{
    public class Server
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "distance")]
        public int Distance { get; set; }
    }
}
