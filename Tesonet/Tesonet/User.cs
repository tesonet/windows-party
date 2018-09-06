using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }

        public static string GetToken(string username, string password)
        {
            string json = JsonConvert.SerializeObject(new User
            {
                username = username,
                password = password
            });

            var response = RequestManager.httpClient
                .PostAsync("tokens", new StringContent(json, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return JObject.Parse(response.Content.ReadAsStringAsync().Result)["token"].ToString();
            else throw new UserAuthenticationException($"User '{username}' was not authenticated, reason : {response.ReasonPhrase}", "Incorrect username or password");
        }
    }
    
    public class UserAuthenticationException : Exception
    {
        public string UiMessage { get; set; }
        public UserAuthenticationException()
        {
        }

        public UserAuthenticationException(string message)
            : base(message)
        {
        }

        public UserAuthenticationException(string message, string uimessage)
            : base(message)
        {
            UiMessage = uimessage;
        }
    }
}
