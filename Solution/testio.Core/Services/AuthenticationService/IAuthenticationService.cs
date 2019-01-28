using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testio.Core.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> Authenticate(string username, string password);
        void Logout();
        string Token { get; }
    }
}
