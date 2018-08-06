using System.Collections.Generic;
using Newtonsoft.Json;

namespace WindowsParty.ApiServices.Models
{
    public abstract class BaseResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class AuthResponse : BaseResponse
    {

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class ServersResponse : BaseResponse
    {
        [JsonProperty("")]
        public  IEnumerable<Server> Servers { get; set; }
    }
}