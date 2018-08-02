using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.services.Servers
{
    public class LoggingServersService : IServersService
    {
        private readonly IServersService _serversService;
        private readonly ILogger _logger;

        public LoggingServersService(IServersService serversService, ILogger logger)
        {
            Ensure.That(serversService, nameof(serversService)).IsNotNull();
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _serversService = serversService;
            _logger = logger;
        }

        public async Task<Server[]> Get(string token)
        {
            try
            {
                _logger.Info("Servers service started with {token}", token);

                var result = await _serversService.Get(token);

                _logger.Info("Servers service finished with {token} and {result}", token, result);

                return result;
            }
            catch(ServersServiceException e)
            {
                _logger.Error("Servers exception occured: {error}", e);
                throw;
            }
            catch(Exception e)
            {
                _logger.Error("Fatal failure: {error}", e);
                throw;
            }
        }
    }
}
