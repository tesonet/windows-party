using WPFApp.Interfaces;

namespace WPFApp.Services
{
    public class TokenService: ITokenService
    {
        public string Token { get; set; }
        public void SaveToken(string token)
        {
            Token = token;
        }
    }
}
