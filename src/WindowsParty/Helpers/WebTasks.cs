using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsParty.Interfaces;
using WindowsParty.Models;

namespace WindowsParty.Helpers
{
    public class WebTasks : IWebTasks
    {
        private RestClient _restClient;
        private List<ServerModel> _serverList;

        public async Task<AuthModel> AuthenticateUser(UserModel userModel)
        {
            try
            {
                //Create rest client based on Base URL
                _restClient = new RestClient(ConfigurationManager.AppSettings["baseUrl"]);

                //Build rest Request
                RestRequest restRequest = BuildAuthRequest(userModel);

                //Execture request
                var restResponse = await _restClient.ExecuteTaskAsync(restRequest);

                //Deserialize object
                return JsonConvert.DeserializeObject<AuthModel>(restResponse.Content);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed calling authentication service: {0}", ex);
            }
        }

        private RestRequest BuildAuthRequest(UserModel userModel)
        {
            var restRequest = new RestRequest(ConfigurationManager.AppSettings["baseUrl"] + ConfigurationManager.AppSettings["tokenPath"], Method.POST, DataFormat.Json)
            {
                JsonSerializer = new Serializer()
            };

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;

            var userModelSerialized = JsonConvert.SerializeObject(userModel, jsonSettings);

            restRequest.AddParameter("application/json", userModelSerialized, ParameterType.RequestBody);

            return restRequest;
        }

        public async Task<List<ServerModel>> RetrieveServerList(AuthModel authModel)
        {
            try
            {
                _restClient = new RestClient(ConfigurationManager.AppSettings["baseUrl"]);

                RestRequest restRequest = BuildServerRequest(authModel);

                var restResponse = await _restClient.ExecuteTaskAsync(restRequest);

                _serverList = JsonConvert.DeserializeObject<List<ServerModel>>(restResponse.Content);

                return _serverList;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed calling server service: {0}", ex);
            }
        }

        private RestRequest BuildServerRequest(AuthModel authModel)
        {
            var restRequest = new RestRequest(ConfigurationManager.AppSettings["baseUrl"] + ConfigurationManager.AppSettings["serverPath"], Method.GET, DataFormat.Json);

            //Add bearer token to the header
            restRequest.AddHeader("Authorization", "Bearer " + authModel.AuthToken);

            return restRequest;
        }
    }
}
