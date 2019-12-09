using System.Threading.Tasks;
using Windowsparty.Model;

namespace WindowsParty.Api
{
    public interface ITokenService
    {
        Task<ApiTokenResponse> GetToken();
    }
}
