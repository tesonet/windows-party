using System.Runtime.Serialization;

namespace Tesonet.Windows_party.Models
{
    [DataContract]
    public class LoginModel
    {
        [DataMember(Name="username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
