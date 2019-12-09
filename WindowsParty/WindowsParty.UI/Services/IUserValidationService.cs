using Windowsparty.Model;

namespace WindowsParty.UI.Services
{
    public interface IUserValidationService
    {
        bool CanExecuteLogin(string userName, string password);
        string ValidateUserInput(User user,string userName, string password);
    }
}