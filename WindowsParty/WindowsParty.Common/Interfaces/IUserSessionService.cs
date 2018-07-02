
using WindowsParty.Common.Models;

namespace WindowsParty.Common.Interfaces
{
    /// <summary>
    /// Service manages logged-in user's info
    /// </summary>
    public interface IUserSessionService
    {
        void AddUser(UserSessionModel user);
        void RemoveUser();
        UserSessionModel GetUser();
    }
}
