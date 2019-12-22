using Newtonsoft.Json;
using RestSharp.Serializers;

namespace WindowsParty.Helpers
{
    public class Serializer : ISerializer
    {
        public string ContentType { get; set; }

        public Serializer()
        {
            ContentType = "application/json";
        }

        public string Serialize(object request)
        {
            return JsonConvert.SerializeObject(request);
        }
    }
}
