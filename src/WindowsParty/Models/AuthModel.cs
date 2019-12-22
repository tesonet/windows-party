using Newtonsoft.Json;

namespace WindowsParty.Models
{
    public class AuthModel
    {
        [JsonProperty("token")]
        public string AuthToken { get; set; }
    }
}
