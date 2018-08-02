using System;
using System.Runtime.Caching;

namespace tesonet.windowsparty.caching
{
    public class Cache<T> : ICache<T>
    {
        public void Add(string key, T value, DateTimeOffset? offset = null)
        {
            try
            {
                if (!MemoryCache.Default.Contains(key))
                {
                    var offsetVal = offset ?? new DateTimeOffset(DateTime.Now.AddMinutes(15));
                    MemoryCache.Default.Add(key, value, offsetVal);
                }
            }
            catch (Exception e)
            {
                throw new CacheException($"Could not store value in cache: key {key}, value {value}", e);
            }
        }

        public bool TryGet(string key, out T value)
        {
            value = default(T);

            try
            {
                if (MemoryCache.Default.Contains(key))
                {
                    value = (T)MemoryCache.Default.Get(key);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new CacheException($"Could not get value from cache: key {key}, value {value}", e);
            }
        }
    }
}
