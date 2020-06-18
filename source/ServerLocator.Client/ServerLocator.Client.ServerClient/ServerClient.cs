using Caliburn.Micro;
using Newtonsoft.Json;
using ServerLocator.Client.Shared;
using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly ILog logger;

        public ServerClient(HttpClient httpClient, ClientApiConfig apiConfig, ILog logger)
        {
            this.httpClient = httpClient;
            this.apiConfig = apiConfig;
            this.logger = logger;
        }

        public async Task<bool> TryAuthenticateAsync(NetworkCredential credentials)
        {
            try
            {
                logger.Info($"About to authenticate. User: '{credentials.UserName}'");

                var tokenRequestContent = BiuldTokenRequestContent(credentials);
                using (var response = await httpClient.PostAsync(apiConfig.TokensEndpoint, tokenRequestContent))
                {
                    response.EnsureSuccessStatusCode();
                    var token = JsonConvert.DeserializeObject<AuthToken>(await response.Content.ReadAsStringAsync());
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Token);
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.Warn($"Authentication failed. User: '{credentials.UserName}'", ex);
                return false;
            }
        }

        public async Task<IEnumerable<ServerModel>> GetServersAsync()
        {
            try
            {
                logger.Info("About to get servers list");
                using (var serversResponse = await httpClient.GetAsync(apiConfig.ServersEndpoint))
                {
                    var serversJson = await serversResponse.Content.ReadAsStringAsync();
                    var servers = JsonConvert.DeserializeObject<List<ServerModel>>(serversJson);

                    return servers;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        private HttpContent BiuldTokenRequestContent(NetworkCredential credentials)
        {
            var contentType = "application/json";
            var serializedCredentials = JsonConvert.SerializeObject(new { username = credentials.UserName, password = credentials.Password });
            var requestContent = new StringContent(serializedCredentials, Encoding.UTF8, contentType);

            return requestContent;
        }
    }
}

