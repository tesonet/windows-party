using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WindowsPartyApp.Api;
using WindowsPartyApp.Model;

namespace WindowsPartyApp.Tests.Api
{
    [TestClass]
    public class ApiTests
    {
        private const string userName = "tesonet";
        private const string password = "partyanimal";

        [TestMethod]
        public void Test_If_Login_With_Correct_Credentials_Works()
        {
            var credentials = new Credentials { UserName = userName, Password = password };
            var tesonetApi = new TesonetApi();

            var result = tesonetApi.Login(credentials).Result;

            Assert.IsFalse(string.IsNullOrEmpty(result.Token));
        }

        [TestMethod]
        public void Test_If_Login_With_InCorrect_Credentials_Not_Works()
        {
            var credentials = new Credentials { UserName = userName, Password = password + "_" };
            var tesonetApi = new TesonetApi();

            var result = tesonetApi.Login(credentials).Result;

            Assert.IsTrue(string.IsNullOrEmpty(result.Token));
        }

        [TestMethod]
        public void Test_If_Get_Servers_With_Correct_Credentials_Works()
        {
            var credentials = new Credentials { UserName = userName, Password = password };
            var tesonetApi = new TesonetApi();

            var token = tesonetApi.Login(credentials).Result;

            var result = tesonetApi.GetServers(token.Token).Result;

            Assert.IsTrue(result.Count() > 0);
        }
    }
}
