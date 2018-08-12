using System.Threading.Tasks;

namespace Tesonet.ServerProxy.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<string> GetAuthToken(string url, string userName, string userPassword);
    }
}