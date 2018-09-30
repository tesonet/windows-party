using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Clients.Contracts;
using WindowsParty.Models;
using WindowsParty.Services.Contracts;

namespace WindowsParty.Services
{
    public class SessionService : ISessionService
    {
        private AuthToken token;
        private IApiClient apiClient { get; set; }

        public SessionService(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<bool> Login(string username, string password)
        {
            var tokenResponse = await apiClient.PostToken(username, password);
            if (tokenResponse.StatusCode != System.Net.HttpStatusCode.OK) return false;
            token = tokenResponse.Data;
            return true;
        }

        public async Task<string> GetToken()
        {
            return token?.Token;
        }

        public async Task Logout()
        {
            token = null;
        }
    }
}
