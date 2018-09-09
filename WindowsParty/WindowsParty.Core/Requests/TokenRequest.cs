using WindowsParty.Core.Responses;
using MediatR;

namespace WindowsParty.Core.Requests
{
    public class TokenRequest : IRequest<TokenResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public TokenRequest() { }

        public TokenRequest(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
