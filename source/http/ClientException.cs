using System;

namespace tesonet.windowsparty.http
{
    public class ClientException : Exception
    {
        public ClientException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
