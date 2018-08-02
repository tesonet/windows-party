using EnsureThat;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.data;
using tesonet.windowsparty.data.Tokens;

namespace tesonet.windowsparty.services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IQueryHandler<Credentials, TokenResponse> _queryHandler;

        public AuthenticationService(IQueryHandler<Credentials, TokenResponse> queryHandler)
        {
            Ensure.That(queryHandler, nameof(queryHandler)).IsNotNull();

            _queryHandler = queryHandler;
        }

        public async Task<string> Authenticate(Credentials credentials)
        {
            Ensure.That(credentials, nameof(credentials)).IsNotNull();
            Ensure.That(credentials.Username, nameof(credentials.Username)).IsNotNullOrWhiteSpace();
            Ensure.That(credentials.Password, nameof(credentials.Password)).IsNotNullOrWhiteSpace();

            try
            {
                var result =
                    await _queryHandler.Get(
                        new TokenQuery
                        {
                            Payload = credentials
                        });

                return result.Token;
            }
            catch (TokenQueryException e)
            {
                throw new AuthenticationServiceException("Authentication failed", e);
            }
        }
    }
}
