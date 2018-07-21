using System.Runtime.Serialization;

namespace Tesonet.Windows_party.Models
{
    [DataContract]
    public class TokenModel
    {
        [DataMember(Name="token")]
        public string Token { get; set; }
    }
}