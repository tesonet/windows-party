using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WindowsParty.ApiServices.Models;
using Newtonsoft.Json;

namespace WindowsParty.ApiServices
{
    public interface IPlaygroundService
    {
        /// <summary>
        /// Authorizes user and gets API token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthResponse> Authorize(AuthRequest request);

        /// <summary>
        /// Gets all servers
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ServersResponse> Servers(string token);
    }

    public class PlaygroundService : IPlaygroundService
    {
        private static Uri _base = new Uri("http://playground.tesonet.lt/v1");

        /// <inheritdoc />
        public async Task<AuthResponse> Authorize(AuthRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (var c = new HttpClient())
            {
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var content = new StringContent(req);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var res = await c.PostAsync($"{_base}/tokens", content);

                if (!res.IsSuccessStatusCode)
                    throw new Exception("Failed to authorize. Status code: " + res.StatusCode);

                var rv = await res.Content.ReadAsStringAsync();

                return DeserializeObject<AuthResponse>(rv);
            }
        }

        /// <inheritdoc />
        public async Task<ServersResponse> Servers(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));

            using (var c = new HttpClient())
            {
                c.DefaultRequestHeaders.Add("Authorization", token);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var res = await c.GetAsync($"{_base}/servers");
                if (!res.IsSuccessStatusCode)
                    throw new Exception("Failed to get servers. Status code: " + res.StatusCode);

                var rv = await res.Content.ReadAsStringAsync();

                try
                {
                    var servers = DeserializeObject<Server[]>(rv);
                    return new ServersResponse() { Servers = servers };
                }
                catch (Exception e)
                {
                    var r = DeserializeObject<ServersResponse>(rv);
                    return r;
                }
            }
        }


        private static T DeserializeObject<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
