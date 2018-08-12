namespace Tesonet.Windows.Party.Models
{
    public class User
    {
        public string Username { get; private set; }
        public string Token { get; private set; }

        public bool IsLoggedIn { get => !string.IsNullOrEmpty(Token); }

        public User(string username, string token)
        {
            Username = username;
            Token = token;
        }
    }
}
