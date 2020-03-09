using System.Threading;
using System.Threading.Tasks;

namespace Teso.Windows.Party.Clients.Authentication
{
    public interface IAuthenticationClient
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
    }
}