using System.Threading.Tasks;
using TesonetWinParty.Models;

namespace TesonetWinParty.Helpers
{
    public interface IAccountHelper
    {
        TokenItem Token { get; }

        Task LogIn(string username, string password);
        void LogOut();
    }
}