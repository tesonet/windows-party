using RestSharp.Deserializers;

namespace ServerLister.Service.Interfaces.Dto
{
    public class ServerDto
    {
        [DeserializeAs(Name = "Name")]
        public string ServerName { get; set; }

        public double Distance { get; set; }
    }
}