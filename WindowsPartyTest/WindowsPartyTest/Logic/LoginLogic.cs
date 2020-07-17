using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Client.Base;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.Models;

namespace WindowsPartyTest.Logic
{
    public class LoginLogic
    {
        ILoginService _service = null;
        public LoginLogic(ILoginService loginService)
        {
            _service = loginService;
        }

        public bool Login(UserData userData)
        {
            return _service.Login(userData);
        }
        public void LogOut()
        {
            _service.LogOut();
        }
    }
}
