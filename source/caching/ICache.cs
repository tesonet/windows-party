using System;

namespace tesonet.windowsparty.caching
{
    public interface ICache<T>
    {
        void Add(string key, T value, DateTimeOffset? offset = null);

        bool TryGet(string key, out T value);
    }
}
