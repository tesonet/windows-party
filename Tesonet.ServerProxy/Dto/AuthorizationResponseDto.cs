using Newtonsoft.Json;

namespace Tesonet.ServerProxy.Dto
{
    [JsonObject]
    public class AuthorizationResponseDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}