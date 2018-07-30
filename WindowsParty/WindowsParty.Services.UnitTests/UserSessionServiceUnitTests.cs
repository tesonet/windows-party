
using WindowsParty.Common.Models;
using Xunit;

namespace WindowsParty.Services.UnitTests
{
    public class UserSessionServiceUnitTests
    {
        [Fact]
        public void AddUser_Ok()
        {
            var service = new UserSessionService();

            service.AddUser(new UserSessionModel
            {
                Username = "username",
                Token = "token"
            });

            var user = service.GetUser();

            Assert.True(user.Username == "username");
        }

        [Fact]
        public void RemoveUser_Ok()
        {
            var service = new UserSessionService();

            service.AddUser(new UserSessionModel
            {
                Username = "username",
                Token = "token"
            });

            service.RemoveUser();

            var user = service.GetUser();

            Assert.Null(user);
        }
    }
}
