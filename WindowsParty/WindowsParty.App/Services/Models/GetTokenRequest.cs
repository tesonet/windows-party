
using Newtonsoft.Json;

namespace WindowsParty.App.Services.Models
{
    public class GetTokenRequest
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
