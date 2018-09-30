using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Clients.Contracts;
using WindowsParty.HelpersAndExtensions;
using WindowsParty.Models;
using WindowsParty.Services.Contracts;

namespace WindowsParty.Clients
{
    public class ApiClient : IApiClient
    {
        private readonly string ApiBaseUrl = "http://playground.tesonet.lt/v1";

        public async Task<IRestResponse<List<Server>>> GetServers(string token)
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("servers");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteGetTaskAsync<List<Server>>(request);
            return response;
        }

        public async Task<IRestResponse<AuthToken>> PostToken(string username, string password)
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("tokens");
            request.AddHeader("Content-Type", "application/json");
            request.AddLowerCamelJsonBody(new AuthTokenRequest()
            {
                Password = password,
                Username = username
            });
            var response = await client.ExecutePostTaskAsync<AuthToken>(request);
            return response;
        }
    }
}
