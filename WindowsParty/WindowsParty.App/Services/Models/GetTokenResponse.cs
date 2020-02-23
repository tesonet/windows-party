using Newtonsoft.Json;

namespace WindowsParty.App.Services.Models
{
    public class GetTokenResponse
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
