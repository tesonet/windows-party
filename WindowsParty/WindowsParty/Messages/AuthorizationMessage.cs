namespace WindowsParty.Messages
{
    public class AuthorizationMessage
    {
        public AuthorizationMessage(string token)
        {
            Token = token;
        }

        public bool Authorized => !string.IsNullOrWhiteSpace(Token);

        public string Token { get; }
    }
}
