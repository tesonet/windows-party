using WindowsParty.IRepositories;
using WindowsParty.IServices;

namespace WindowsParty.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        public AuthorizationService(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public string GetAccessToken(string username, string password)
        {
            return _authorizationRepository.GetAccessToken(username, password);
        }
    }
}
