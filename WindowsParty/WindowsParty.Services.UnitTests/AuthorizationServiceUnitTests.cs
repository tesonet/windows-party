
using System.Threading.Tasks;
using WindowsParty.Common.Interfaces;
using Moq;
using WindowsParty.Common.Models;
using Xunit;

namespace WindowsParty.Services.UnitTests
{
    public class AuthorizationServiceUnitTests
    {
        [Fact]
        public async void GenerateToken_Ok()
        {
            var req = new TokenRequestModel
            {
                Username = "username",
                Password = "password"
            };

            var mockResult = new AuthorizationResultModel
            {
                Success = true,
                Token = "token"
            };

            var mockAuthService = new Mock<IAuthorizationService>();
            mockAuthService.Setup(x => x.GenerateToken(It.IsAny<TokenRequestModel>())).Returns(Task.FromResult(mockResult));

            var result = await mockAuthService.Object.GenerateToken(req);

            Assert.True(result.Success && !string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async void GenerateToken_Unauthorized_Ok()
        {
            var req = new TokenRequestModel
            {
                Username = "username",
                Password = "password"
            };

            var mockResult = new AuthorizationResultModel
            {
                Success = false,
                Message = "Unauthorized"
            };

            var mockAuthService = new Mock<IAuthorizationService>();
            mockAuthService.Setup(x => x.GenerateToken(It.IsAny<TokenRequestModel>())).Returns(Task.FromResult(mockResult));

            var result = await mockAuthService.Object.GenerateToken(req);

            Assert.True(!result.Success && result.Message == "Unauthorized");
        }
    }
}
