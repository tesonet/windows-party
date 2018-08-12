using System.Collections.Generic;
using System.Threading.Tasks;
using TesonetWpfApp.Business.Models;

namespace TesonetWpfApp.Business
{
    public interface ITesonetService
    {
        Task<string> GetToken(string username, string password);
        Task<ICollection<Server>> GetServers(string accessToken);
    }
}