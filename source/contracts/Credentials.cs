using Newtonsoft.Json;

namespace tesonet.windowsparty.contracts
{
    public class Credentials
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
