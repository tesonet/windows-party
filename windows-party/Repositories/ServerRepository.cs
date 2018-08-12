using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tesonet.Windows.Party.Models;

namespace Tesonet.Windows.Party.Repositories
{
    public interface IServerRepository
    {
        Task<List<Server>> GetServers(string authToken);
    }

    public class ServerRepository : IServerRepository
    {
        private readonly HttpClient _httpClient;
        private const string ServerListPath = "/v1/servers";

        public ServerRepository(string baseApiUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseApiUrl) };
        }

        public async Task<List<Server>> GetServers(string authToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ServerListPath);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<Server>>();
        }
    }
}
