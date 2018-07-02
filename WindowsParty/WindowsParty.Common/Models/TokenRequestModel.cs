using Newtonsoft.Json;

namespace WindowsParty.Common.Models
{
    [JsonObject]
    public class TokenRequestModel
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
