using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Client.Base;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.Models;

namespace WindowsPartyTest.Client.Services
{
    public class LoginClient : WebClientBase, ILoginService
    {
        public LoginClient(HttpClient client, APIConfig config) : base(client, config)
        {
        }
        public bool Login(UserData userData)
        {
            try
            {
                Log.Debug($"Login started for user {userData.UserID}");
                var request = new { username = userData.UserID, password = new System.Net.NetworkCredential(string.Empty, userData.Password).Password  };
                var data = Task.Run(() => MakePostRequest<TokenData>(_config.TokensEndPoint, request));
                data.Wait();
                TokenData token = data.Result;
                if (token == null || token.Token == string.Empty)
                    return false;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                return true;
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Failed on Login");
            }
            return false;
        }

        public void LogOut()
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = null;
                Log.Debug("Log out successfull");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed on Log out");
            }
        }
    }
}
