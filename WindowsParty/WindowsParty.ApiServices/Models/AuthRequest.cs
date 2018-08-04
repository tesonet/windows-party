using Newtonsoft.Json;

namespace WindowsParty.ApiServices.Models
{
    public class AuthRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}