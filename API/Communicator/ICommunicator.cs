using API.Communicator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace API.Communicator
{
    public interface ICommunicator
    {
        Task<bool> LogIn(string userName, PasswordBox password);
        Task<List<ServerInfoModel>> GetServerList();
    }
}
