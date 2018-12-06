namespace Tesonet.Services
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Http Service
    /// </summary>
    public class HttpService : IHttpService
    {
        /// <summary>
        /// Performs an asynchronous HTTP GET towards the specified service and url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service">The service.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(url);
                
                return await response.Content.ReadAsAsync<T>();
            }
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST with the specified data to the specified service and url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task<T> PostDataAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            var requestArgs = new FormUrlEncodedContent(data);
            var requestMsg = new HttpRequestMessage(HttpMethod.Post, url);
            requestMsg.Content = requestArgs;
      
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.SendAsync(requestMsg);

                return await response.Content.ReadAsAsync<T>();
            }
        }
    }
}
