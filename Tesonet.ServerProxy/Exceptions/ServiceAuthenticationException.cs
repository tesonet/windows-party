using System;

namespace Tesonet.ServerProxy.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }

        public string Content { get; }
    }
}