using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WindowsParty.App.Configurations;

namespace WindowsParty.App.Data
{
    public abstract class HttpDataProvider
    {
        private readonly HttpClient _httpClient;
        private readonly HttpDataConfiguration _httpDataConfiguration;
        private readonly Uri _baseUri;

        protected HttpDataProvider(HttpDataConfiguration httpDataConfiguration)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpDataConfiguration = httpDataConfiguration;
            _baseUri = new Uri(_httpDataConfiguration.ApiUrlBase);
        }

        protected virtual async Task<T> GetDataAsync<T>(HttpRequestMessage message) where T : class
        {
            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        protected HttpRequestMessage GetRequestMessage(
            HttpMethod method, 
            string resourceName,
            HttpContent content = default)
        {
            return new HttpRequestMessage()
            {
                Method = method,
                Content = content,
                RequestUri = new Uri(_baseUri, $"{_httpDataConfiguration.Version}/{resourceName}")
            };
        }

        protected static void AddBearerToken(HttpRequestMessage message, string token)
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

    }
}
