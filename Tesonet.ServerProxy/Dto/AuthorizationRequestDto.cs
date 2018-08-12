using Newtonsoft.Json;

namespace Tesonet.ServerProxy.Dto
{
    [JsonObject]
    public class AuthorizationRequestDto
    {
        public AuthorizationRequestDto(string userName, string userPassword)
        {
            Username = userName;
            Password = userPassword;
        }

        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}