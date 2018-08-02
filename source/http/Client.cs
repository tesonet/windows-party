using EnsureThat;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace tesonet.windowsparty.http
{
    public class Client : IClient
    {
        private readonly string _url;

        public Client(string url)
        {
            Ensure.That(url, nameof(url)).IsNotNullOrWhiteSpace();

            _url = url;
        }

        public async Task<TResult> GetJson<TResult>(string url, string securityToken)
        {
            Ensure.That(url, nameof(url)).IsNotNullOrWhiteSpace();
            Ensure.That(securityToken, nameof(securityToken)).IsNotNullOrWhiteSpace();

            try
            {
                var result = await _url
                    .AppendPathSegment(url)
                    .WithOAuthBearerToken(securityToken)
                    .GetAsync()
                    .ReceiveJson<TResult>();

                return result;
            }
            catch(Exception e)
            {
                throw new ClientException("Error occured during http GET", e);
            }
        }

        public async Task<TResult> PostJson<TBody, TResult>(string url, TBody body)
        {
            Ensure.That(url, nameof(url)).IsNotNullOrWhiteSpace();

            try
            {
                var result = await _url
                    .AppendPathSegment(url)
                    .PostJsonAsync(body)
                    .ReceiveJson<TResult>();

                return result;
            }
            catch (Exception e)
            {
                throw new ClientException("Error occured during http POST", e);
            }
        }
    }
}
