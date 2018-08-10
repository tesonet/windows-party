using System;
using System.Collections.Generic;
using System.Net.Http;
using WindowsParty.IRepositories;
using WindowsParty.Models;
using Newtonsoft.Json;

namespace WindowsParty.Repositories
{
    public class ServerRepository : IServerRepository
    {
        public IEnumerable<Server> GetServers(string accessToken)
        {
            //TODO: get string from config
            using (var client = new HttpClient {BaseAddress = new Uri("http://playground.tesonet.lt/")})
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = client.GetAsync("v1/servers").Result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<IEnumerable<Server>>(result);
            }
        }
    }
}