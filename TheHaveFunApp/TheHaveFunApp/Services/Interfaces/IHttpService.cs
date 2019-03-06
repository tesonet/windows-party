using System.Collections.Generic;
using TheHaveFunApp.Models;

namespace TheHaveFunApp.Services.Interfaces
{
    public interface IHttpService
    {
        IEnumerable<ServerModel> GetServersList();
        bool Login(string userName, string password);
        void Logout();
    }
}
