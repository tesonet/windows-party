using System;

namespace ServerLocator.Client.ServerClient
{
    public class AuthToken
    {
        public AuthToken(string rawToken)
        {
            Token = $"Authorization: Bearer {rawToken}";
            Issued = DateTime.UtcNow;
        }

        public string Token { get; }

        public DateTime Issued { get; }
    }
}
