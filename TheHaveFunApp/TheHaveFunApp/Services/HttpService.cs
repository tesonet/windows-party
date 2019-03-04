using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nx.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheHaveFunApp.Models;

namespace TheHaveFunApp.Services
{
    public class HttpService: IHttpService
    {
        private const string UrlServers = "http://playground.tesonet.lt/v1/servers";
        private const string UrlTokens = "http://playground.tesonet.lt/v1/tokens";
        private string _token;

        public IEnumerable<ServerModel> GetServersList()
        {
            List<ServerModel> _servers = new List<ServerModel>();
            var request = RequestBuilder.Create("default")
                .SetUrl(UrlServers)
                .SetHttpMethod("GET")
                .WithHeader("Authorization", $"Bearer {_token}")
                .Build();

            var response = RequestManager.GetResponse(request);
            if (response.Exception == null)
            {
                _servers = JsonConvert.DeserializeObject<List<ServerModel>>(response.Text);
            }
            else
            {
                throw new Exception("There was error in sending request", response.Exception);
            }

            return _servers;
        }

        public bool Login(string userName, string password)
        {
            userName = "tesonet";
            password = "partyanimal";

            var request = RequestBuilder.Create("default")
                .SetUrl(UrlTokens)
                .SetHttpMethod("POST")
                .WithTextInput("username", userName)
                .WithTextInput("password", password)
                .Build();

            var response = RequestManager.GetResponse(request);
            if (response.Exception == null)
            {
                var data = (JObject)JsonConvert.DeserializeObject(response.Text);
                _token = data["token"].Value<string>();
                return true;
            }
            else
            {
                throw new Exception("There was error in sending request", response.Exception);
            }
        }

        public void Logout()
        {
            _token = string.Empty;            
        }
    }
}
