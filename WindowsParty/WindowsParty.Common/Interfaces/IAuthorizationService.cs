using System.Threading.Tasks;
using WindowsParty.Common.Models;

namespace WindowsParty.Common.Interfaces
{
    /// <summary>
    /// Service manages tokens
    /// </summary>
    public interface IAuthorizationService
    {
        Task<AuthorizationResultModel> GenerateToken(TokenRequestModel req);
    }
}
