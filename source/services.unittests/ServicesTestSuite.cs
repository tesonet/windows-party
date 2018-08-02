using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.data;
using tesonet.windowsparty.data.Servers;
using tesonet.windowsparty.data.Tokens;
using tesonet.windowsparty.services.Authentication;
using tesonet.windowsparty.services.Servers;

namespace tesonet.windowsparty.services.unittests
{
    [TestClass]
    public class ServicesTestSuite
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AuthenticationService_Constructor_QueryHandlerIsNull_ShouldThrowArgumentNullException()
        {
            // arrange and act
            var authenticationsService = new AuthenticationService(null);

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ServersService_Constructor_QueryHandlerIsNull_ShouldThrowArgumentNullException()
        {
            // arrange and act
            var serversService = new ServersService(null);

            // assert ExpectedException
        }

        [TestMethod]
        public async Task AuthenticationService_Authenticate_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var mockQueryHandler = new Mock<IQueryHandler<Credentials, TokenResponse>>();
            var authenticationService = new AuthenticationService(mockQueryHandler.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => authenticationService.Authenticate(null));
            //await Assert.ThrowsExceptionAsync<ArgumentException>(() => authenticationService.Authenticate("", null));
            //await Assert.ThrowsExceptionAsync<ArgumentException>(() => authenticationService.Authenticate(" ", null));
            //await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => authenticationService.Authenticate("some", null));
            //await Assert.ThrowsExceptionAsync<ArgumentException>(() => authenticationService.Authenticate("some", ""));
            //await Assert.ThrowsExceptionAsync<ArgumentException>(() => authenticationService.Authenticate("some", " "));
        }

        [TestMethod]
        public async Task ServersService_Get_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var mockQueryHandler = new Mock<IQueryHandler<string, Server[]>>();
            var serversService = new ServersService(mockQueryHandler.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => serversService.Get(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => serversService.Get(""));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => serversService.Get(" "));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationServiceException))]
        public async Task AuthenticationService_Authenticate_OnTokenQueryException_ShouldThrowAuthenticationException()
        {
            // arrange
            var mockQueryHandler = new Mock<IQueryHandler<Credentials, TokenResponse>>();

            mockQueryHandler.Setup(q => q.Get(It.IsAny<TokenQuery>())).ThrowsAsync(new TokenQueryException());

            var authenticationService = new AuthenticationService(mockQueryHandler.Object);

            // act
            var result = await authenticationService.Authenticate(new Credentials { Username = "some", Password = "some" });

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ServersServiceException))]
        public async Task ServersService_Get_OnServerQueryException_ShouldThrowServersException()
        {
            // arrange
            var mockQueryHandler = new Mock<IQueryHandler<string, Server[]>>();

            mockQueryHandler.Setup(q => q.Get(It.IsAny<ServerQuery>())).ThrowsAsync(new ServerQueryException());

            var serversService = new ServersService(mockQueryHandler.Object);

            // act
            var result = await serversService.Get("token");

            // assert ExpectedException
        }

        [TestMethod]
        public async Task AuthenticationService_Authenticate_ShouldReturnToken()
        {
            // arrange
            var mockQueryHandler = new Mock<IQueryHandler<Credentials, TokenResponse>>();
            var credentials = new Credentials { Username = "some", Password = "some" };

            mockQueryHandler.Setup(q => q.Get(It.IsAny<TokenQuery>())).ReturnsAsync(new TokenResponse { Token = "token" });

            var authenticationService = new AuthenticationService(mockQueryHandler.Object);

            // act
            var result = await authenticationService.Authenticate(credentials);

            // assert
            Assert.AreEqual("token", result);
        }

        [TestMethod]
        public async Task ServersService_Get_ShouldReturnServers()
        {
            // arrange
            var mockQueryHandler = new Mock<IQueryHandler<string, Server[]>>();
            var token = "token";

            mockQueryHandler.Setup(q => q.Get(It.IsAny<ServerQuery>())).ReturnsAsync(new Server[] { new Server { Name = "Lietuva" } });

            var serversService = new ServersService(mockQueryHandler.Object);

            // act
            var result = await serversService.Get(token);

            // assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Lietuva", result[0].Name);
        }
    }
}
