using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testio.Core.Services.AuthenticationService
{
    public class AuthenticationResult
    {
        public AuthenticationResult(AuthenticationResultType authenticationResultType)
        {
            Result = authenticationResultType;
        }

        public AuthenticationResult(AuthenticationResultType authenticationResultType, Exception error)
        {
            Result = authenticationResultType;
            Error = error;
        }

        public AuthenticationResultType Result { get; private set; }
        public Exception Error { get; private set; }
    }

    public enum AuthenticationResultType
    {
        Success,
        EmailOrPasswordIsIncorrect,
        Error
    }
}
