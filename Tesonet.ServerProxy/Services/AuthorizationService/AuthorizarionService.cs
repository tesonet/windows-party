using System.Threading.Tasks;
using Tesonet.ServerProxy.Dto;
using Tesonet.ServerProxy.Services.RequestProvider;

namespace Tesonet.ServerProxy.Services.AuthorizationService
{
    public class AuthorizarionService : IAuthorizationService
    {
        private readonly IRequestProvider _requestProvider;
        public AuthorizarionService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<string> GetAuthToken(string url, string userName, string userPassword)
        {
            var request = new AuthorizationRequestDto(userName, userPassword);
            var response = await _requestProvider.PostAsync<AuthorizationResponseDto>(url, request);
            return response.Token;
        }
    }
}