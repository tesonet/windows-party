using System.Collections.Generic;
using System.Threading.Tasks;
using TesonetWinParty.Models;

namespace TesonetWinParty.Helpers
{
    public interface IAPIHelper
    {
        Task<TokenItem> AuthenticateAsync(string username, string password);

        Task<List<Server>> GetServersList(string token);
    }
}