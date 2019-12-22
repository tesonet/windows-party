
using Newtonsoft.Json;

namespace WindowsParty.Models
{
    public class UserModel
    {
        public UserModel(string _username, string _password)
        {
            Username = _username;
            Password = _password;
        }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
