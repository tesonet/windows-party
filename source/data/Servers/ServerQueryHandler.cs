using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.http;

namespace tesonet.windowsparty.data.Servers
{
    public class ServerQueryHandler : IQueryHandler<string, Server[]>
    {
        private readonly IClient _client;

        public ServerQueryHandler(IClient client)
        {
            Ensure.That(client, nameof(client)).IsNotNull();

            _client = client;
        }

        public async Task<Server[]> Get(IQuery<string> query)
        {
            Ensure.That(query, nameof(query)).IsNotNull();
            Ensure.That(query.Payload, nameof(query.Payload)).IsNotNullOrWhiteSpace();

            try
            {
                var result = await _client.GetJson<Server[]>("servers", query.Payload);

                return result;
            }
            catch(Exception e)
            {
                throw new ServerQueryException("Could not retrieve server list", e);
            }
        }
    }
}
