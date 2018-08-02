using EnsureThat;
using System;
using System.Text;
using System.Threading.Tasks;
using tesonet.windowsparty.caching;
using tesonet.windowsparty.contracts;

namespace tesonet.windowsparty.data.Tokens
{
    public class CachingTokenQueryHandler : IQueryHandler<Credentials, TokenResponse>
    {
        private readonly IQueryHandler<Credentials, TokenResponse> _queryHandler;
        private readonly ICache<TokenResponse> _cache;

        public CachingTokenQueryHandler(IQueryHandler<Credentials, TokenResponse> queryHandler, ICache<TokenResponse> cache)
        {
            Ensure.That(queryHandler, nameof(queryHandler)).IsNotNull();
            Ensure.That(queryHandler, nameof(cache)).IsNotNull();

            _queryHandler = queryHandler;
            _cache = cache;
        }

        public async Task<TokenResponse> Get(IQuery<Credentials> query)
        {
            var cacheKey = GenerateCacheKey(query.Payload);

            if (_cache.TryGet(cacheKey, out var result))
            {
                return result;
            }

            result = await _queryHandler.Get(query);
            _cache.Add(query.Payload.Username, result);

            return result;
        }

        private static string GenerateCacheKey(Credentials credentials)
        {
            var joined = $"{credentials.Username}_{credentials.Password}";
            var bytes = Encoding.ASCII.GetBytes(joined);

            return Convert.ToBase64String(bytes);
        }
    }
}
