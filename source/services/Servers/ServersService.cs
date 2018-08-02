using EnsureThat;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.data;
using tesonet.windowsparty.data.Servers;

namespace tesonet.windowsparty.services.Servers
{
    public class ServersService : IServersService
    {
        private readonly IQueryHandler<string, Server[]> _queryHandler;

        public ServersService(IQueryHandler<string, Server[]> queryHandler)
        {
            Ensure.That(queryHandler, nameof(queryHandler)).IsNotNull();

            _queryHandler = queryHandler;
        }

        public async Task<Server[]> Get(string token)
        {
            Ensure.That(token, nameof(token)).IsNotNullOrWhiteSpace();

            try
            {
                var result =
                    await _queryHandler.Get(
                        new ServerQuery
                        {
                            Payload = token
                        });

                return result;
            }
            catch (ServerQueryException e)
            {
                throw new ServersServiceException("Getting servers failed", e);
            }
        }
    }
}
