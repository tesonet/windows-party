using Caliburn.Micro;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TesonetWinParty.Helpers;
using TesonetWinParty.Models;
using TesonetWinParty.ViewModels;

namespace TesonetWinParty.Tests
{
    [TestFixture]
    public class ApiHelperTest
    {
        private const string _userName = "tesonet";
        private const string _password = "partyanimal";
        APIHelper _apiHelper;

        [SetUp]
        public void Setup()
        {
            _apiHelper = new APIHelper();
        }

        [Test]
        public async Task Test_TokenSuccessfullyReceived()
        {
           TokenItem result = await _apiHelper.AuthenticateAsync(_userName, _password);
           Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Token));
        }

        [Test]
        public void Test_BadLoginThrowsException()
        {
            Assert.That(async () => await _apiHelper.AuthenticateAsync("tes", "party"), Throws.Exception);
        }

        [Test]
        public async Task Test_GetServersList()
        {
            TokenItem token = await _apiHelper.AuthenticateAsync(_userName, _password);
            var servers = await _apiHelper.GetServersList(token.Token);
            Assert.NotZero(servers.Count, "Error while returning servers");
        }
    }
}
