using WindowsParty.Common.Interfaces;
using WindowsParty.Common.Models;

namespace WindowsParty.Services
{
    public class UserSessionService : IUserSessionService
    {
        private UserSessionModel _user;

        public void AddUser(UserSessionModel user)
        {
            _user = user;
        }

        public UserSessionModel GetUser()
        {
            return _user;
        }

        public void RemoveUser()
        {
            _user = null;
        }
    }
}
