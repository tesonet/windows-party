using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Teso.Windows.Party.Clients.Authentication
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public AuthenticationClient(HttpClient httpClient, JsonSerializerSettings jsonSerializerSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonSerializerSettings = jsonSerializerSettings ?? throw new ArgumentNullException(nameof(jsonSerializerSettings));
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(request, _jsonSerializerSettings), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("tokens", stringContent).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                response.Dispose();
                throw new Exception("Authentication Error");
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthenticationResponse>(result, _jsonSerializerSettings);
        }
    }
}