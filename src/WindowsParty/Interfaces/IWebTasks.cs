using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.Models;

namespace WindowsParty.Interfaces
{
    public interface IWebTasks
    {
        Task<AuthModel> AuthenticateUser(UserModel userModel);

        Task<List<ServerModel>> RetrieveServerList(AuthModel authModel);
    }
}
