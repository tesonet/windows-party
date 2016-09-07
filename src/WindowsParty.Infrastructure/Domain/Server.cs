using System;
using Newtonsoft.Json;

namespace WindowsParty.Infrastructure.Domain
{
    public class Server : IEquatable<Server>
    {
        public string Name { get; set; }
        public string Distance { get; set; }
        public bool Equals(Server other)
        {
            return ToString() == other.ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
