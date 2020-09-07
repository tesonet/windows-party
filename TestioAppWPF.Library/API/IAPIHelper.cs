using System.Threading.Tasks;
using TestioApp_WPF.Models;
using TestioAppWPF.Library.API;


namespace TestioAppWPF.Library.API
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}