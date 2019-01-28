using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using authService = testio.Services.AuthenticationService;
using testio.Core.Services.AuthenticationService;

namespace testio.Services.Tests.AuthenticationService
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private authService.AuthenticationService _authenticationService = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _authenticationService = new authService.AuthenticationService();
        }

        [TestMethod]
        public void CheckValidCredentialsReturn()
        {
            var result = _authenticationService.Authenticate("tesonet", "partyanimal");

            var authenticationResult = result.Result;

            Assert.AreEqual<AuthenticationResultType>(AuthenticationResultType.Success, authenticationResult.Result);
            Assert.IsNotNull(_authenticationService.Token);
            Assert.IsNull(authenticationResult.Error);            
        }

        [TestMethod]
        public void CheckInvalidCredentialsReturn()
        {
            var result = _authenticationService.Authenticate("tesonet", "partyanimal1");

            var authenticationResult = result.Result;

            Assert.AreEqual<AuthenticationResultType>(AuthenticationResultType.EmailOrPasswordIsIncorrect, authenticationResult.Result);
            Assert.IsNull(_authenticationService.Token);
            Assert.IsNull(authenticationResult.Error);            
        }
    }
}
