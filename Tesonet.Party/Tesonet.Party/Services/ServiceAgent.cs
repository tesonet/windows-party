using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tesonet.Party.Contracts;

namespace Tesonet.Party.Services
{
    public interface ITesonetServiceAgent
    {
        Task<SessionToken> Login(string username, string password);
        Task<List<Server>> GetServers(string token);
    }

    public class TesonetServiceAgent : ITesonetServiceAgent
    {
        private static string baseServiceUri = "http://playground.tesonet.lt/";

        public async Task<SessionToken> Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseServiceUri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });
                var result = await client.PostAsync("/v1/tokens", content);
                return await result.Content.ReadAsAsync<SessionToken>();
            }
        }

        public async Task<List<Server>> GetServers(string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseServiceUri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", token);

                var result = await client.GetAsync("/v1/servers");
                return await result.Content.ReadAsAsync<List<Server>>();
            }
        }
    }
}
