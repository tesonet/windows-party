using System;

namespace tesonet.windowsparty.services.Authentication
{
    public class AuthenticationServiceException : Exception
    {
        public AuthenticationServiceException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
