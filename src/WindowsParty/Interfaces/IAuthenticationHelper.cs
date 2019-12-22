using System.Threading.Tasks;
using WindowsParty.Models;

namespace WindowsParty.Interfaces
{
    public interface IAuthenticationHelper
    {
        Task<AuthModel> AuthenticateUser(UserModel userModel);

        AuthModel AuthModel{ get; }

        void LogOut();
    }
}
