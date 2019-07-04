using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesonetWinParty.Models;

namespace TesonetWinParty.Helpers
{
    public class AccountHelper : IAccountHelper
    {
        private TokenItem _token;
        private IAPIHelper _apiHelper;

        public AccountHelper(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public TokenItem Token
        {
            get { return _token; }
            private set { _token = value; }
        }

        public async Task LogIn(string username, string password)
        {
            try
            {
                TokenItem tokenResponse = await _apiHelper.AuthenticateAsync(username, password);
                if (!String.IsNullOrWhiteSpace(tokenResponse.Token))
                {
                    Token = tokenResponse;                  
                }
                else
                {
                    throw new Exception("Empty token received");
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Equals("Unauthorized"))
                {
                    throw new Exception("Bad login or password");
                }
                else
                {
                    throw new Exception(ex.Message);
                }                                   
            }
           
            
        }

        public void LogOut()
        {
            Token = null;
        }

    }
}
