using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WindowsParty.IRepositories;

namespace WindowsParty.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        public string GetAccessToken(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://playground.tesonet.lt/");
                
                var response = client.PostAsync("v1/tokens",
                    new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password)
                    })
                );
                var result = response.Result.Content.ReadAsStringAsync().Result;
                var deserializeObject = JsonConvert.DeserializeObject<AccessToken>(result);
                return deserializeObject.Token;
            }

        }

        class AccessToken
        {
            public string Token { get; set; }
        }
    }
}
