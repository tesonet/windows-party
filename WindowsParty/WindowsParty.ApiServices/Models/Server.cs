using Newtonsoft.Json;

namespace WindowsParty.ApiServices.Models
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distance")]
        public long Distance { get; set; }
    }

    public class TokenRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
