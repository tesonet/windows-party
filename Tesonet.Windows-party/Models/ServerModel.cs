using System.Runtime.Serialization;

namespace Tesonet.Windows_party.Models
{
    [DataContract]
    public class ServerModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "distance")]
        public int Distance { get; set; }
    }
}