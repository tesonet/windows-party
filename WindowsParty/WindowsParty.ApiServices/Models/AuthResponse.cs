using Newtonsoft.Json;

namespace WindowsParty.ApiServices.Models
{
    public class AuthResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}