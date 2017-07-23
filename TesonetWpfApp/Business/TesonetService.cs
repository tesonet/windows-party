using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using TesonetWpfApp.Business.Models;

namespace TesonetWpfApp.Business
{
    public class TesonetService : ITesonetService
    {
        private readonly Uri _baseUrl = new Uri("http://playground.tesonet.lt/v1");

        private readonly IRestService _restClient;

        public TesonetService(IRestService restClient)
        {
            _restClient = restClient;
        }

        public async Task<string> GetToken(string username, string password)
        {
            IRestRequest request = new RestRequest("tokens", Method.POST);
            request.AddJsonBody(new { username = username, password = password });

            var response = await _restClient.Execute<TokenResponse>(_baseUrl, request);
            return response.Token;
        }

        public async Task<ICollection<Server>> GetServers(string accessToken)
        {
            IRestRequest request = new RestRequest("servers");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            var response = await _restClient.Execute<List<Server>>(_baseUrl, request);
            return response;
        }
    }
}
