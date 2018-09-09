using System.Configuration;
using WindowsParty.Core.External;
using WindowsParty.Core.External.PlaygroundClient;
using WindowsParty.Core.Requests;
using NUnit.Framework;

namespace WindowsParty.IntegrationTests
{
    [TestFixture]
    public class PlaygroundClientTests
    {
        private string _token;
        private readonly IPlaygroundClient _playgroundClient;

        public PlaygroundClientTests()
        {
            _playgroundClient = new PlaygroundClient();
        }

        [Test]
        [Order(1)]
        public void Should_Return_Token_When_Credentials_Are_Valid()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = ConfigurationManager.AppSettings["PlaygroundUserName"],
                Password = ConfigurationManager.AppSettings["PlaygroundPassword"]
            };

            var response = _playgroundClient.GetToken(tokenRequest).Result;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Token);
            Assert.IsNotEmpty(response.Token);
            _token = response.Token;
        }

        [Test]
        [Order(2)]
        public void Should_Return_Null_When_Credentials_Are_Invalid()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = ConfigurationManager.AppSettings["PlaygroundUserName"],
                Password = "bad one"
            };

            Assert.ThrowsAsync<ClientException>(() => _playgroundClient.GetToken(tokenRequest));
        }

        [Test]
        [Order(3)]
        public void Should_Return_List_Of_Servers()
        {
            var serverListRequest = new ServerListRequest { Token = _token };

            var response = _playgroundClient.GetServerList(serverListRequest).Result;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Servers);
            CollectionAssert.IsNotEmpty(response.Servers);
        }
    }
}
