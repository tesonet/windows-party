using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nx.Net;
using System;
using System.Collections.Generic;
using TheHaveFunApp.Services.Interfaces;
using TheHaveFunApp.Models;

namespace TheHaveFunApp.Services
{
    public class HttpService : IHttpService
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
            try
            {
                var response = RequestManager.GetResponse(request);
                if (response.Exception == null)
                {
                    _servers = JsonConvert.DeserializeObject<List<ServerModel>>(response.Text);                    
                }
                else
                {
                    throw new Exception("GetServersList: There was error in sending request", response.Exception);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetServersList: There was error in sending request", ex);
            }

            return _servers;
        }

        public bool Login(string userName, string password)
        {
            var request = RequestBuilder.Create("default")
                .SetUrl(UrlTokens)
                .SetHttpMethod("POST")
                .WithTextInput("username", userName)
                .WithTextInput("password", password)
                .Build();

            try
            {
                var response = RequestManager.GetResponse(request);
                if (response.Exception == null)
                {
                    var data = (JObject)JsonConvert.DeserializeObject(response.Text);
                    _token = data["token"].Value<string>();
                    return true;
                }
                else
                {
                    throw new Exception("Login: There was error in sending request", response.Exception);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login: There was error in sending request", ex);
            }
        }

        public void Logout()
        {
            _token = string.Empty;
        }
    }
}
