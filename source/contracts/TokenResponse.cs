using Newtonsoft.Json;

namespace tesonet.windowsparty.contracts
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
