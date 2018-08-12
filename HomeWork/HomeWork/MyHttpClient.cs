using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HomeWork
    {
    //public interface IMyHttpClient
    //    {
    //    List<string> GetServers ();
    //    }

    public class MyHttpClient : IDisposable
        {
        private const string BASE_URL = @"http://playground.tesonet.lt/v1/";
        private const string TOKENS = "tokens";
        private const string SERVERS = "servers";
        private const string CONTENT_TYPE = "application/json";
        private const string TOKEN_TYPE = "Bearer";

        private const string USERNAME = "username";
        private const string PASSWORD = "password";
        private const string TOKEN = "token";
        private const string NAME = "name";
        private const string DISTANCE = "distance";

        private string _username;
        private string _password;
        private string _token;
        private HttpClient _client;

        public MyHttpClient (string userName, string password)
            {
            _username = userName;
            _password = password;
            _client = new HttpClient ();
            }

        public async Task SetToken ()
            {
            var request = new HttpRequestMessage ()
                {
                RequestUri = new Uri ($"{BASE_URL}{TOKENS}"),
                Method = HttpMethod.Post,
                Content = new StringContent (new JObject (
                    new JProperty (USERNAME, _username),
                    new JProperty (PASSWORD, _password)).ToString (), Encoding.UTF8, CONTENT_TYPE)
                };
            var response = await _client.SendAsync (request);

            if ( response.StatusCode == HttpStatusCode.Unauthorized )
                throw new ArgumentException ("Bad credentials.");

            JObject responseJson = JObject.Parse (await response.Content.ReadAsStringAsync ());
            _token = responseJson[TOKEN].ToString ();
            }

        public async Task<List<Server>> GetServers ()
            {
            var request = new HttpRequestMessage ()
                {
                RequestUri = new Uri ($"{BASE_URL}{SERVERS}"),
                Method = HttpMethod.Get
                };
            request.Headers.Authorization = new AuthenticationHeaderValue (TOKEN_TYPE, _token);

            var response = await _client.SendAsync (request);
            return JObjectToServers(await response.Content.ReadAsStringAsync ());
            }

        public void Dispose ()
            {
            Dispose (true);
            GC.SuppressFinalize (this);
            }

        protected virtual void Dispose (bool disposing)
            {
            if (disposing)
                _client.Dispose ();
            }

        private static List<Server> JObjectToServers (string jsonString)
            {
            List<Server> servers = new List<Server> ();

            JToken json = JToken.Parse (jsonString);
            foreach ( JToken token in json.Children() )
                servers.Add (new Server (token[NAME].ToString(), token[DISTANCE].ToString()));

            return servers;
            }
        }
    }
