using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tesonet.Windows_party.Models;
using Tesonet.Windows_party.Services.Interfaces;

namespace Tesonet.Windows_party.Services.Implementations
{
    public class ApiService : IApiService
    {
        private const string ApiUrl = "http://playground.tesonet.lt/v1";
        private readonly ILoggerService _loggerService;
        private string _authToken;

        public ApiService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_authToken);

        public async Task<bool> Login(LoginModel loginModel)
        {
            var requestUri = $"{ApiUrl}/tokens";
            var content = GenerateStringHttpContent(loginModel);
            var tokenModel = await PostAsync<TokenModel>(requestUri, content);
            if (!string.IsNullOrWhiteSpace(tokenModel.Token))
            {
                _authToken = tokenModel.Token;
                _loggerService.Info($"User {loginModel.UserName} is sucessfuly authenticated");
                return true;
            }

            return false;
        }      

        public async Task<List<ServerModel>> GetServers()
        {
            _loggerService.Info("Get servers called");
            var requestUri = $"{ApiUrl}/servers";
            if (IsAuthenticated)
            {
                return await GetAsync<List<ServerModel>>(requestUri);
            }

            return new List<ServerModel>();
        }

        public void Logout()
        {
            _loggerService.Info($"User log out called");
            _authToken = null;
        }

        private static StringContent GenerateStringHttpContent<T>(T jsonModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(jsonModel));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            return content;
        }

        private async Task<T> PostAsync<T>(string requestUri, HttpContent content)
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(requestUri, content);
                var stringResult = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(stringResult);
            }
        }

        private async Task<T> GetAsync<T>(string requestUri)
        {
            using (var httpClient = new HttpClient())
            {
                if (IsAuthenticated)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
                }
                
                var result = await httpClient.GetAsync(requestUri);
                var stringResult = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(stringResult);
            }
        }
    }
}