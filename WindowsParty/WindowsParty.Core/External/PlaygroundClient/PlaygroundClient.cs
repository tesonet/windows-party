using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Threading.Tasks;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace WindowsParty.Core.External.PlaygroundClient
{
    public class PlaygroundClient : IPlaygroundClient
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(PlaygroundClient));
        private readonly string _baseUri;

        public PlaygroundClient()
        {
            _baseUri = ConfigurationManager.AppSettings["PlaygroundUri"];

            if (string.IsNullOrEmpty(_baseUri))
                throw new ArgumentException(nameof(_baseUri));
        }

        public async Task<TokenResponse> GetToken(TokenRequest tokenRequest)
        {
            var client = new RestClient($"{_baseUri}/tokens");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=password&username={tokenRequest.UserName}&password={tokenRequest.Password}",
                ParameterType.RequestBody);
            var response = await client.ExecuteTaskAsync(request);

            return HandleResponse<TokenResponse>(response);
        }



        public async Task<ServerListResponse> GetServerList(ServerListRequest serverListRequest)
        {
            var client = new RestClient($"{_baseUri}/servers");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + serverListRequest.Token);
            var response = await client.ExecuteTaskAsync<IList<ServerInfo>>(request); // does not work with array:/
            var data = HandleResponse<IList<ServerInfo>>(response);

            return new ServerListResponse(data);
        }

        private TResponse HandleResponse<TResponse>(IRestResponse response) where TResponse : class
        {
            // if status code is between 400 and 500 (client error) throw validation exception
            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                throw new ClientException(response.Content);

            // if status is greater or equals 500 (server error)
            if ((int)response.StatusCode >= 500)
            {
                _log.ErrorFormat("Playground client server error. Response: {0}", response.Content);
                throw new ValidationException("Server error, please try again in a few seconds");
            }

            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }
    }
}
