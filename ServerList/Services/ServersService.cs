using Caliburn.Micro;
using ServerList.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServerList.Utils
{
    public class ServersService : IServersService
    {
        private readonly string _serversListUrl;
        private readonly HttpClient _httpClient;
        private readonly ILog _logger;

        public ServersService(HttpClient httpClient, IConfig config, ILog logger)
        {
            _httpClient = httpClient;
            _serversListUrl = config.Get("ServersUrl");
            _logger = logger;
        }

        public async Task<HttpResponseMessage> GetServersListAsync(string authorizationToken)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);

            _logger.Info($"Getting server list from {_serversListUrl}");
            HttpResponseMessage response = await _httpClient.GetAsync(_serversListUrl);

            return response;
        }
    }
}
