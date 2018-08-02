using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.data.Tokens
{
    public class LoggingTokenQueryHandler : IQueryHandler<Credentials, TokenResponse>
    {
        private readonly IQueryHandler<Credentials, TokenResponse> _queryHandler;
        private readonly ILogger _logger;

        public LoggingTokenQueryHandler(IQueryHandler<Credentials, TokenResponse> queryHandler, ILogger logger)
        {
            Ensure.That(queryHandler, nameof(queryHandler)).IsNotNull();
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _queryHandler = queryHandler;
            _logger = logger;
        }

        public async Task<TokenResponse> Get(IQuery<Credentials> query)
        {
            try
            {
                var result = await _queryHandler.Get(query);

                return result;
            }
            catch(TokenQueryException e)
            {
                _logger.Error("Error occured in quering token: {exception}", e);
                throw;
            }
            catch(Exception e)
            {
                _logger.Error("Fatal failure: {exception}", e);
                throw;
            }
        }
    }
}
