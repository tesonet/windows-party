using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerLocator.Client.Shared;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServerLocator.Client.ServerClient
{
    public class ServerClient : IServerClient
    {
        private readonly HttpClient httpClient;
        private readonly ClientApiConfig apiConfig;
        private readonly ILogger<ServerClient> logger;

        public ServerClient(HttpClient httpClient, ClientApiConfig apiConfig, ILogger<ServerClient> logger)
        {
            this.httpClient = httpClient;
            this.apiConfig = apiConfig;
            this.logger = logger;
            this.httpClient.BaseAddress = new Uri(apiConfig.BaseUrl);
        }

        public async Task<bool> TryAuthenticateAsync(ClientCredentials credentials)
        {
            try
            {
                logger.LogDebug($"About to authenticate. User: '{credentials.Username}'");
                var tokenRequestContent = BiuldTokenRequestContent(credentials);
                var response = await httpClient.PostAsync(apiConfig.TokensEndpoint, tokenRequestContent);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var authToken = new AuthToken(content);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("https", authToken.Token);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Authentication failed. User: '{credentials.Username}'", ex);
                return false;
            }
        }

        private HttpContent BiuldTokenRequestContent(ClientCredentials credentials)
        {
            var contentType = "application/json";
            var serializedCredentials = JsonConvert.SerializeObject(credentials);
            var requestContent = new StringContent(serializedCredentials, Encoding.UTF8, contentType);

            return requestContent;
        }
    }
}

