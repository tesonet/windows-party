using System.Net;

namespace WindowsParty.Infrastructure.Communication
{
    public interface IAuthenticator
    {
        string Token { get; }

        HttpStatusCode Authenticate(string username, string password);
    }
}
