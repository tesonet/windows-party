using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WindowsPartyApp.Model;

namespace WindowsPartyApp.Api
{
    public class TesonetApi : IApi
    {
        private const string LoginUrl = "http://playground.tesonet.lt/v1/tokens";
        private const string ServersUrl = "http://playground.tesonet.lt/v1/servers";

        public async Task<AuthToken> Login(Credentials credentials)
        {
            var content = new StringContent(JsonConvert.SerializeObject(credentials));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(LoginUrl, content);

                var stringResult = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<AuthToken>(stringResult);
            }
        }

        public async Task<IEnumerable<Server>> GetServers(string authToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var result = await httpClient.GetAsync(ServersUrl);

                var stringResult = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Server>>(stringResult);
            }
        }
    }
}
