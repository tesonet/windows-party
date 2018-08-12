using System.Collections.Generic;
using Tesonet.Domain.Domain;

namespace Tesonet.Client.Models
{
    public class CountryServers
    {
        public string Country { get; set; }
        public List<Server> Servers { get; set; }
    }
}