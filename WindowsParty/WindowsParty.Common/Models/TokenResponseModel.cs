using Newtonsoft.Json;

namespace WindowsParty.Common.Models
{
    public class TokenResponseModel
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
