using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Interfaces;
using WPFApp.Models;
using log4net;
using System.Reflection;

namespace WPFApp.Services
{
    public class HttpService: IHttpService
    {
        private readonly ITokenService _tokenService;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly HttpClient _httpClient = new HttpClient();
        public HttpService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenService> LogIn(User user)
        {
            _logger.Info($"Lets get Token with username: {user.username} and password: {user.password}");
            var requestUri = new Uri(ConfigurationManager.AppSettings["TesoTokenURL"].ToString());

            var requestContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var requestMessage = GetHttpRequestMessage(HttpMethod.Post, requestUri, requestContent);
            var response = await GetTAsync<TokenService>(requestMessage);

           
            if (!string.IsNullOrEmpty(response.Token))
            {
                _tokenService.SaveToken(response.Token);
            }

            return response;
        }

        public async Task<IEnumerable<ServerModel>> GetServerList()
        {
            var requestUri = new Uri(ConfigurationManager.AppSettings["TesoServerURL"].ToString());
            var httpRequest = GetHttpRequestMessage(HttpMethod.Get, requestUri,default);

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.Token);

            var serverList = await GetTAsync<IEnumerable<ServerModel>>(httpRequest);
            
            if (!serverList.Any())
            {
                _logger.Info("Sorry, server list is empty");
            }

            return serverList;
        }

        public async Task<T> GetTAsync<T>(HttpRequestMessage requestMessage)
        {
            var response = await _httpClient.SendAsync(requestMessage);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    _logger.Error($"Authorizataion completed: {response.ReasonPhrase}");
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized:
                    _logger.Error($"Error authorization: {response.ReasonPhrase}");
                    return default;
                default: 
                    return default;
            }
        }

        private HttpRequestMessage GetHttpRequestMessage(HttpMethod method, Uri requestUri, HttpContent content)
        {
            return new HttpRequestMessage()
            {
                Method = method,
                RequestUri = requestUri,
                Content = content
            };
        }

    }
}
