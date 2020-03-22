using System.Threading.Tasks;
using WindowsParty.App.Models.V1;

namespace WindowsParty.App.Interfaces
{
    public interface IUserService
    {
        Task<Bearer> GetToken(string userName, string password);
    }
}
