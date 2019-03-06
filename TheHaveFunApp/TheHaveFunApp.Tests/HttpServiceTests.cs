using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheHaveFunApp.Services;
using TheHaveFunApp.Services.Interfaces;

namespace TheHaveFunApp.Tests
{
    [TestClass]
    public class HttpServiceTests
    {
        [TestMethod]
        public void GetServers()
        {
            IHttpService service = new HttpService();
            service.Login("tesonet", "partyanimal");
            var list = service.GetServersList();
            Assert.AreNotEqual(0, list.Count(), "List is not empty");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetServersAfterLogout()
        {
            IHttpService service = new HttpService();
            service.Login("tesonet", "partyanimal");
            var list = service.GetServersList();
            Assert.AreNotEqual(0, list.Count(), "List is not empty");
            service.Logout();
            list = service.GetServersList();
            Assert.AreNotEqual(0, list.Count(), "List is not empty");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetServersWithoutToken()
        {
            IHttpService service = new HttpService();
            var list = service.GetServersList();
            Assert.AreNotEqual(0, list.Count(), "List is not empty");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetServersWrongLogin()
        {
            IHttpService service = new HttpService();
            service.Login("tesonet", "fakePassword");
            var list = service.GetServersList();
            Assert.AreNotEqual(0, list.Count(), "List is not empty");
        }
    }
}
