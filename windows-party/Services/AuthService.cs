using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Tesonet.Windows.Party.Models;

namespace Tesonet.Windows.Party.Services
{
    public interface IAuthService
    {
        Task<string> LogIn(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private const string LogInPath = "/v1/tokens";

        public AuthService(string baseApiUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseApiUrl) };
        }

        public async Task<string> LogIn(string username, string password)
        {
            var response = await _httpClient.PostAsync(
                LogInPath, 
                new {  username,  password}, 
                new JsonMediaTypeFormatter(), 
                "application/json")
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var authResponse = await response.Content.ReadAsAsync<AuthResponse>().ConfigureAwait(false);

            return authResponse.Token;
        }
    }
}
