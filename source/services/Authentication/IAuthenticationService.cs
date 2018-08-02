using System.Threading.Tasks;
using tesonet.windowsparty.contracts;

namespace tesonet.windowsparty.services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(Credentials credentials);
    }
}
