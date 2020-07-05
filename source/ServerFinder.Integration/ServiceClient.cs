namespace ServerFinder.Integration
{
    using Newtonsoft.Json;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class ServiceClient : IServiceClient
    {
        private HttpClient _httpClient;

        public ServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> TryLogIn(NetworkCredential credentials, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information($"Authenticating user {credentials.UserName}.");
                var contentString = JsonConvert.SerializeObject(new { username = credentials.UserName, password = credentials.Password });
                var requestContent = new StringContent(contentString, Encoding.UTF8, "application/json");

                using (var response = await _httpClient.PostAsync("tokens", requestContent, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    var token = JsonConvert.DeserializeObject<AuthorizationToken>(await response.Content.ReadAsStringAsync());
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                }

                Log.Information($"Successfully authenticated user {credentials.UserName}.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to authenticate user {credentials.UserName}.", ex);
                return false;
            }
        }

        public async Task<IEnumerable<Server>> GetServerList(CancellationToken cancellationToken) 
        {
            try
            {
                Log.Information($"Retrieving servers list.");
                using (var response = await _httpClient.GetAsync("servers", cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    var servers = JsonConvert.DeserializeObject<List<Server>>(await response.Content.ReadAsStringAsync());

                    Log.Information($"Successfully retrieved {servers.Count} servers.");
                    return servers;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to retrieve servers list.", ex);
                throw;
            }
        }

        public void LogOut()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            Log.Information($"User has logged out.");
        }
    }
}
