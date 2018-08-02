using System;

namespace tesonet.windowsparty.data.Servers
{
    public class ServerQueryException : Exception
    {
        public ServerQueryException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
