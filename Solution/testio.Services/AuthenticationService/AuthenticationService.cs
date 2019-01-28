using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testio.Core.Services.AuthenticationService;
using System.Net;
using RestSharp;
using System.Threading;

namespace testio.Services.AuthenticationService
{
    public class AuthenticationService: IAuthenticationService
    {
        // pk: app.Config, IConfigService ???
        private const string URL = "http://playground.tesonet.lt/v1/tokens";

        public async Task<AuthenticationResult> Authenticate(string username, string password)
        {
            try
            {
                //await Task.Delay(5000);

                var client = new RestClient(URL);

                var request = new RestRequest(Method.POST);
                request.Parameters.Clear();
                request.AddParameter("username", username);
                request.AddParameter("password", password);
                request.AddParameter("Content-Type", "application/json");

                var cancellationTokenSource = new CancellationTokenSource();
                var response = await client.ExecuteTaskAsync<LoginToken>(request, cancellationTokenSource.Token);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        Token = response.Data.Token;
                        return new AuthenticationResult(AuthenticationResultType.Success);
 
                    case HttpStatusCode.Unauthorized:
                        return new AuthenticationResult(AuthenticationResultType.EmailOrPasswordIsIncorrect);

                    default:
                        return new AuthenticationResult(AuthenticationResultType.Error, new Exception(response.StatusCode.ToString()));
                }
            }
            catch (Exception e)
            {
                return new AuthenticationResult(AuthenticationResultType.Error, e);
            }            
        }

        public void Logout()
        {
            Token = null;            
        }

        #region Properties

        public string Token { get; private set; }

        #endregion Properties
    }
}
