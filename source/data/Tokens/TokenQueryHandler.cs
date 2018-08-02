using EnsureThat;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.http;

namespace tesonet.windowsparty.data.Tokens
{
    public class TokenQueryHandler : IQueryHandler<Credentials, TokenResponse>
    {
        private readonly IClient _client;

        public TokenQueryHandler(IClient client)
        {
            Ensure.That(client, nameof(client)).IsNotNull();

            _client = client;
        }

        public async Task<TokenResponse> Get(IQuery<Credentials> query)
        {
            Ensure.That(query, nameof(query)).IsNotNull();
            Ensure.That(query.Payload, nameof(query.Payload)).IsNotNull();

            try
            {
                var result = await _client.PostJson<Credentials, TokenResponse>("tokens", query.Payload);

                return result;
            }
            catch(Exception e)
            {
                throw new TokenQueryException("Could not retrieve token", e);
            }
        }
    }
}
