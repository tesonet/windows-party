using System;

namespace tesonet.windowsparty.data.Tokens
{
    public class TokenQueryException : Exception
    {
        public TokenQueryException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
