using System.Threading.Tasks;

namespace ServerLocator.Client.ServerClient
{
    public interface IServerClient
    {
        Task<bool> TryAuthenticateAsync(ClientCredentials credentials);
    }
}
