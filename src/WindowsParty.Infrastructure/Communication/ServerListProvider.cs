using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.Infrastructure.Domain;
using Newtonsoft.Json;
using RestSharp;

namespace WindowsParty.Infrastructure.Communication
{
    public class ServerListProvider : IServerListProvider
    {
        private readonly IRestClient _client;

        public ServerListProvider(IRestClient client)
        {
            _client = client;
        }

        public IList<Server> GetServers(string token)
        {
            var request = new RestRequest("servers", Method.GET);
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = _client.Execute(request);
            if (response == null) return null;
            return JsonConvert.DeserializeObject<List<Server>>(response?.Content ?? "");
        }

        public RestRequestAsyncHandle GetServersAsync(string token, Action<List<Server>> callback)
        {
            var request = new RestRequest("servers", Method.GET);
            request.AddHeader("Authorization", $"Bearer {token}");

            return _client.ExecuteAsync<List<Server>>(request, (response) =>
            {
                callback(response.Data);
            });
        }

    }
}
