using System.Runtime.Serialization;

namespace WindowsPartyApp.Model
{
    [DataContract]
    public class Credentials
    {
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
