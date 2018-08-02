using System.Threading.Tasks;

namespace tesonet.windowsparty.http
{
    public interface IClient
    {
        /// <summary>
        /// Makes Http.GET call to resource by <paramref name="url"/> with bearer <paramref name="securityToken"/> added.
        /// </summary>
        /// <typeparam name="TResult">The type of response body.</typeparam>
        /// <param name="url">Url to the resource.</param>
        /// <param name="securityToken">Bearer token.</param>
        /// <returns>The content of the response.</returns>
        /// <exception cref="ClientException">Throws exception if communication fails.</exception>
        Task<TResult> GetJson<TResult>(string url, string securityToken);

        /// <summary>
        /// Makes Http.POST call to resource by <paramref name="url"/>.
        /// </summary>
        /// <typeparam name="TBody">The type of request body</typeparam>
        /// <typeparam name="TResult">The type of response body.</typeparam>
        /// <param name="url">Url to the resource.</param>
        /// <param name="body">Request body.</param>
        /// <returns>The content of the response.</returns>
        /// <exception cref="ClientException">Throws exception if communication fails.</exception>
        Task<TResult> PostJson<TBody, TResult>(string url, TBody body);
    }
}
