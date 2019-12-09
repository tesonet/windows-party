using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;

namespace WindowsParty.UI.Services
{
    public class UserValidationService : IUserValidationService
    {
        public bool CanExecuteLogin(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            return true;
        }

        public string ValidateUserInput(User user, string userName, string password)
        {
            var result = "";
            if (user == null)
            {
                return "User Name does not exist";
            }
            if (user.UserName != userName)
            {
                return "Wrong Username";
            }
            if (user.Password != password)
            {
                return "Wrong password";
            }
            return result;
        }
    }
}
