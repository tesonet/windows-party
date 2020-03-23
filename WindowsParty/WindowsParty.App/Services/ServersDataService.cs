using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsParty.App.Configurations;
using WindowsParty.App.Data;
using WindowsParty.App.Interfaces;
using WindowsParty.App.Models.V1;

namespace WindowsParty.App.Services
{
    public class ServersDataService : HttpDataProvider, IServersDataService
    {
        public ServersDataService(HttpDataConfiguration httpDataConfiguration)
            : base(httpDataConfiguration)
        { }

        public async Task<IEnumerable<Server>> GetServers()
        {
            var resourceName = "servers";
            var message = GetRequestMessage(HttpMethod.Get, resourceName);
            AddBearerToken(message, UserSession.BearerToken);

            return await GetDataAsync<IEnumerable<Server>>(message);
        }
    }
}
