using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Teso.Windows.Party.Models;

namespace Teso.Windows.Party.Clients.ServerList
{
    public class ServerListClient : IServerListClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ServerListClient(HttpClient httpClient, JsonSerializerSettings jsonSerializerSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonSerializerSettings = jsonSerializerSettings ?? throw new ArgumentNullException(nameof(jsonSerializerSettings));
        }

        public async Task<IEnumerable<Server>> GetServerList(string token, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync("servers", cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                response.Dispose();
                throw new Exception("Server list error");
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Server>>(result, _jsonSerializerSettings);
        }
    }
}