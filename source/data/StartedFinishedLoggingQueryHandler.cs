using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.data
{
    public class StartedFinishedLoggingQueryHandler<TPayload, TResult> : IQueryHandler<TPayload, TResult>
    {
        public readonly IQueryHandler<TPayload, TResult> _queryHandler;
        public readonly ILogger _logger;

        public StartedFinishedLoggingQueryHandler(IQueryHandler<TPayload, TResult> queryHandler, ILogger logger)
        {
            Ensure.That(queryHandler, nameof(queryHandler)).IsNotNull();
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _queryHandler = queryHandler;
            _logger = logger;
        }

        public async Task<TResult> Get(IQuery<TPayload> query)
        {
            try
            {
                _logger.Info("Handling {query}: started", query);

                var result = await _queryHandler.Get(query);

                _logger.Info("Handling {query}: finished with {result}", query, result);

                return result;
            }
            catch(Exception e)
            {
                _logger.Error("Error occured: {exception}", e);
                throw;
            }
        }
    }
}
