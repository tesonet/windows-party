using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.data.Servers
{
    public class LoggingServerQueryHandler : IQueryHandler<string, Server[]>
    {
        private readonly IQueryHandler<string, Server[]> _queryHandler;
        private readonly ILogger _logger;

        public LoggingServerQueryHandler(IQueryHandler<string, Server[]> queryHandler, ILogger logger)
        {
            Ensure.That(queryHandler, nameof(queryHandler)).IsNotNull();
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _queryHandler = queryHandler;
            _logger = logger;
        }

        public async Task<Server[]> Get(IQuery<string> query)
        {
            try
            {
                var result = await _queryHandler.Get(query);

                return result;
            }
            catch (ServerQueryException e)
            {
                _logger.Error("Error occured in quering server list: {exception}", e);
                throw;
            }
            catch (Exception e)
            {
                _logger.Error("Fatal failure: {exception}", e);
                throw;
            }
        }
    }
}
