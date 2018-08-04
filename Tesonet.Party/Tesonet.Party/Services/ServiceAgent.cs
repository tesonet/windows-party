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
        Task<LoginResult> Login(string username, string password);
        Task<GetServersResult> GetServers(string token);
    }

    public class TesonetServiceAgent : ITesonetServiceAgent
    {
        private static string baseServiceUri = "http://playground.tesonet.lt/v1/";

        public async Task<LoginResult> Login(string username, string password)
        {
            try
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
                    var response = await client.PostAsync("tokens", content);
#if DEBUG
                    await Task.Delay(1500);
#endif
                    return await response.Content.ReadAsAsync<LoginResult>();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandler.LogException(ex);
                return new LoginResult() { Message = ex.Message };
            }
        }

        public async Task<GetServersResult> GetServers(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseServiceUri);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("Authorization", token);

                    var response = await client.GetAsync("servers");

#if DEBUG
                    await Task.Delay(1500);
#endif
                    if (response.IsSuccessStatusCode)
                    {
                        var servers = await response.Content.ReadAsAsync<List<Server>>();
                        return new GetServersResult() { Servers = servers };
                    }
                    else
                        return await response.Content.ReadAsAsync<GetServersResult>();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandler.LogException(ex);
                return new GetServersResult() { Message = ex.Message };
            }
        }
    }
}
