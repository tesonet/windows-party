using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Client.Base;
using WindowsPartyTest.Models;

namespace WindowsPartyTest.Client.Services.Interfaces
{
    public interface ILoginService
    {
        bool Login(UserData userData);
        void LogOut();
    }
}
