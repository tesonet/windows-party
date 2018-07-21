using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tesonet.Windows_party.Models;
using Tesonet.Windows_party.Services.Implementations;
using Tesonet.Windows_party.Services.Interfaces;

namespace Tesonet.Windows_party.Tests.Services
{
    [TestFixture]
    public class ApiServiceIntegrationTests
    {
        private readonly ILoggerService _loggerService = Substitute.For<ILoggerService>();
        private IApiService _apiService;

        [SetUp]
        public void SetUp()
        {
            _apiService = new ApiService(_loggerService);
        }

        [Test]
        public async Task Should_ReturnTrue_WhenCallingLoginWithGoodCredentials()
        {
            // arrange
            var userName = "tesonet";
            var password = "partyanimal";
            var loginModel = new LoginModel {UserName = userName, Password = password};

            // act
            var result = await _apiService.Login(loginModel);

            // assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Should_ReturnFalse_WhenCallingLoginWithBadCredentials()
        {
            // arrange
            var userName = "badUserName";
            var password = "badPassword";
            var loginModel = new LoginModel { UserName = userName, Password = password };

            // act
            var result = await _apiService.Login(loginModel);

            // assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task Should_ReturnServers_WhenCallingGetServersAfterLogin()
        {
            //act
            await SuccesfulLogin();
            var servers = await _apiService.GetServers();

            // assert
            servers.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task Should_ReturnFalse_WhenCallingIsAuthenticatedAfterLogout()
        {
            //act
            await SuccesfulLogin();
            _apiService.Logout();

            // assert
            _apiService.IsAuthenticated.Should().BeFalse();
        }

        private async Task SuccesfulLogin()
        {
            var userName = "tesonet";
            var password = "partyanimal";
            var loginModel = new LoginModel {UserName = userName, Password = password};
            
            await _apiService.Login(loginModel);
        }
    }
}
