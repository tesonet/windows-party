using Caliburn.Micro;
using Newtonsoft.Json;
using ServerList.Interfaces;
using ServerList.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServerList.Utils
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _loginUrl;
        private readonly HttpClient _httpClient;
        private readonly ILog _logger;

        public AuthenticationService(HttpClient httpClient, IConfig config, ILog logger)
        {
            _httpClient = httpClient;
            _loginUrl = config.Get("LoginUrl");
            _logger = logger;
        }

        public async Task<HttpResponseMessage> LoginAsync(string username, string password)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string credentialsJson = JsonConvert.SerializeObject(new Credentials(username, password));
            StringContent data = new StringContent(credentialsJson, Encoding.UTF8, "application/json");

            _logger.Info($"Sending Login request to {_loginUrl}");
            HttpResponseMessage response = await _httpClient.PostAsync(_loginUrl, data);

            return response;
        }
    }
}
