using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.http
{
    public class LoggingClient : IClient
    {
        private readonly IClient _client;
        private readonly ILogger _logger;

        public LoggingClient(IClient client, ILogger logger)
        {
            Ensure.That(client, nameof(client)).IsNotNull();
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _client = client;
            _logger = logger;
        }

        public async Task<TResult> GetJson<TResult>(string url, string securityToken)
        {
            try
            {
                _logger.Info("Http GET started: {url}, {securityToken}", url, securityToken);

                var result = await _client.GetJson<TResult>(url, securityToken);

                _logger.Info("Http GET finished: {url}, {securityToken}, {result}", url, securityToken, result);

                return result;
            }
            catch(ClientException e)
            {
                _logger.Error("Error occured in http client: {exception}", e);
                throw;
            }
            catch(Exception e)
            {
                _logger.Error("Fatal failure in http client: {exception}", e);
                throw;
            }
        }

        public async Task<TResult> PostJson<TBody, TResult>(string url, TBody body)
        {
            try
            {
                _logger.Info("Http POST started: {url}, {body}", url, body);

                var result = await _client.PostJson<TBody, TResult>(url, body);

                _logger.Info("Http POST finished: {url}, {body}, {result}", url, body, result);

                return result;
            }
            catch (ClientException e)
            {
                _logger.Error("Error occured in http client: {exception}", e);
                throw;
            }
            catch (Exception e)
            {
                _logger.Error("Fatal failure in http client: {exception}", e);
                throw;
            }
        }
    }
}
