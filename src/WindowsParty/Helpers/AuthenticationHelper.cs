using System;
using System.Threading.Tasks;
using WindowsParty.Interfaces;
using WindowsParty.Models;

namespace WindowsParty.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private AuthModel _authModel;
        private IWebTasks _webTasks;

        public AuthModel AuthModel
        {
            get { return _authModel; }
            private set { _authModel = value; }
        }

        public AuthenticationHelper(IWebTasks webTasks)
        {
            _webTasks = webTasks ?? throw new ArgumentNullException(nameof(webTasks));
        }

        public async Task<AuthModel> AuthenticateUser(UserModel userModel)
        {
            try
            {
                var authResponse = await _webTasks.AuthenticateUser(userModel);

                //Check if the returned authentication token is not empty
                if (!String.IsNullOrWhiteSpace(authResponse.AuthToken))
                {
                    return AuthModel = authResponse;
                }
                else
                {
                    throw new Exception("Retrieved empty authentication token");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed calling authentication service");
            }
        }

        public void LogOut()
        {
            AuthModel = null;
        }
    }
}
