using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.services.Authentication
{
    public class LoggingAuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger _logger;

        public LoggingAuthenticationService(IAuthenticationService authenticationService, ILogger logger)
        {
            Ensure.That(authenticationService, nameof(authenticationService)).IsNotNull();
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _authenticationService = authenticationService;
            _logger = logger;
        }

        public async Task<string> Authenticate(Credentials credentials)
        {
            try
            {
                _logger.Info("Authentication service started with {username}", credentials.Username);

                var result = await _authenticationService.Authenticate(credentials);

                _logger.Info("Authentication service finished for {username} with {result}", credentials.Username, result);

                return result;
            }
            catch(AuthenticationServiceException e)
            {
                _logger.Error("Authentication exception occured: {error}", e);
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
