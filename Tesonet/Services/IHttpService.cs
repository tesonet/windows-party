namespace Tesonet.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///   Http Service
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Performs an asynchronous HTTP GET towards the specified service and url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns>Response data</returns>
        Task<T> GetAsync<T>(string url, string token);
        
        /// <summary>
        /// Performs an asynchronous HTTP POST with the specified data to the specified service and url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>Response data</returns>
        Task<T> PostDataAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> data);
    }
}
