using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsPartyTest.Client.Base
{
    public class WebClientBase
    {
        protected readonly HttpClient _client;
        protected readonly APIConfig _config;
        public WebClientBase(HttpClient client, APIConfig config)
        {
            _config = config;
            _client = client;
        }

        public async Task<TReturnType> MakePostRequest<TReturnType>(string url, object param, CancellationToken cancellationToken = default)
        {
            TReturnType data = default(TReturnType);
            try
            {
                DateTime dStart = DateTime.Now;
                Log.Debug($"MakePostRequest url = [{url}]");
                string json = JsonConvert.SerializeObject(param);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage result = await _client.PostAsync(url, stringContent, cancellationToken);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var responseStream = await result.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(responseStream))
                        {
                            data = JsonConvert.DeserializeObject<TReturnType>(responseStream);
                        }
                    }
                }
                Log.Debug($"MakePostRequest url = [{url}] completed with IsSuccessStatusCode = [{result.IsSuccessStatusCode}] duration = [{(DateTime.Now - dStart)}]");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Make Post Request Failed");
            }
            return data;
        }

        public async Task<TReturnType> MakeGetRequest<TReturnType>(string url, CancellationToken cancellationToken = default)
        {
            TReturnType data = default(TReturnType);
            try
            {
                DateTime dStart = DateTime.Now;
                Log.Debug($"MakeGetRequest url = [{url}]");


                HttpResponseMessage result = await _client.GetAsync(url, cancellationToken);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var responseStream = await result.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(responseStream))
                        {
                            data = JsonConvert.DeserializeObject<TReturnType>(responseStream);
                        }
                    }
                }
                Log.Debug($"MakeGetRequest url = [{url}] completed with IsSuccessStatusCode = [{result.IsSuccessStatusCode}] duration = [{(DateTime.Now - dStart)}]");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Make Get Request Failed");
            }
            return data;
        }
    }
}
