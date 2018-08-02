using System;

namespace tesonet.windowsparty.caching
{
    public class CacheException : Exception
    {
        public CacheException(string message = "", Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
