
namespace WindowsParty.App.Domain.Commands
{
    public class LoginUserCommand : ICommand
    {
        public string Username { get; }
        public string Password { get; }

        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
