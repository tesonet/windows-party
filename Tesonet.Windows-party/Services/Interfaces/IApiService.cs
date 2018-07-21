using System.Collections.Generic;
using System.Threading.Tasks;
using Tesonet.Windows_party.Models;

namespace Tesonet.Windows_party.Services.Interfaces
{
    public interface IApiService
    {
        bool IsAuthenticated { get; }

        Task<bool> Login(LoginModel loginModel);

        Task<List<ServerModel>> GetServers();

        void Logout();        
    }
}
