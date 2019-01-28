using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testio.Core.Services.AuthenticationService;
using testio.Core.Services.ServersService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using authService = testio.Services.AuthenticationService;
using serversService = testio.Services.ServersService;

namespace testio.Services.Tests.ServersService
{
    [TestClass]
    public class ServersServiceTests
    {
        private authService.AuthenticationService _authenticationService = null;
        private serversService.ServersService _serversService = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _authenticationService = new authService.AuthenticationService();
            var result = _authenticationService.Authenticate("tesonet", "partyanimal");

            _serversService = new serversService.ServersService();
        }

        [TestMethod]
        public void CheckValidCredentialsReturn()
        {
            var result = _serversService.GetServerList(_authenticationService.Token);

            Assert.IsNotNull(result.Result);
        }
    }
}
