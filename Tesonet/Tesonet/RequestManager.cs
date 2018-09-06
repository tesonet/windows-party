using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet
{
    public class RequestManager
    {
        public static readonly HttpClient httpClient = GetDefaultHttpClient();

        private static HttpClient GetDefaultHttpClient()
        {
            if (httpClient == null)
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://playground.tesonet.lt/v1/");
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }
            return httpClient;
        }
    }
}
