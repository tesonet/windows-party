using Caliburn.Micro;
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
        private readonly ILog log = LogManager.GetLog(typeof(TesonetApi));

        public async Task<AuthToken> Login(Credentials credentials)
        {
            log.Info("Login started with username: {0} and password: {1}", credentials.UserName, credentials.Password);

            var content = new StringContent(JsonConvert.SerializeObject(credentials));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(LoginUrl, content);

                var stringResult = await result.Content.ReadAsStringAsync();

                log.Info("Login finished with username: {0} and password: {1}", credentials.UserName, credentials.Password);

                return JsonConvert.DeserializeObject<AuthToken>(stringResult);
            }
        }

        public async Task<IEnumerable<Server>> GetServers(string authToken)
        {
            log.Info("Get servers started with token: {0}", authToken);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var result = await httpClient.GetAsync(ServersUrl);

                var stringResult = await result.Content.ReadAsStringAsync();

                log.Info("Get servers fonished with token: {0}", authToken);

                return JsonConvert.DeserializeObject<IEnumerable<Server>>(stringResult);
            }
        }
    }
}
