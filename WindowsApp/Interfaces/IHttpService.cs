using System.Collections.Generic;
using System.Threading.Tasks;
using WPFApp.Models;
using WPFApp.Services;

namespace WPFApp.Interfaces
{
    public interface IHttpService
    {
        Task<TokenService> LogIn(User user);
        Task<IEnumerable<ServerModel>> GetServerList();
    }
}
