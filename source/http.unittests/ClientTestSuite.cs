using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tesonet.windowsparty.contracts;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace tesonet.windowsparty.http.unittests
{
    [TestClass]
    public class ClientTestSuite
    {
        private FluentMockServer _server;

        [TestInitialize]
        public void Initialize()
        {
            _server = FluentMockServer.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Client_Constructor_UrlIsNull_ShouldThrowArgumentNullException()
        {
            // arrange and act
            var client = new Client(null);

            // assert ExpectedException
        }

        [TestMethod]
        public async Task Client_GetJson_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var client = new Client(_server.Urls.First());

            // act and assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => client.GetJson<string>(null, null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetJson<string>("", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetJson<string>(" ", null));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => client.GetJson<string>("some", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetJson<string>("some", ""));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetJson<string>("some", " "));
        }

        [TestMethod]
        public async Task Client_PostJson_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var client = new Client(_server.Urls.First());

            // act and assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => client.PostJson<int, string>(null, 0));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.PostJson<int, string>("", 0));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.PostJson<int, string>(" ", 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public async Task Client_GetJson_OnException_ShouldThrowClientException()
        {
            // arrange
            var client = new Client(_server.Urls.First());

            _server
                .Given(Request.Create().WithPath("/servers").UsingGet())
                .RespondWith(Response.Create().WithNotFound());

            // act
            var result = await client.GetJson<Server[]>("servers", "token");

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public async Task Client_PostJson_OnException_ShouldThrowClientException()
        {
            // arrange
            var client = new Client(_server.Urls.First());

            _server
                .Given(Request.Create().WithPath("/tokens").UsingPost())
                .RespondWith(Response.Create().WithNotFound());

            // act
            var result = await client.PostJson<Credentials, TokenResponse>("tokens", null);

            // assert ExpectedException
        }

        [TestMethod]
        public async Task Client_GetJson_ShouldReturnValidResult()
        {
            var client = new Client(_server.Urls.First());

            _server
                .Given(Request.Create().WithPath("/servers").UsingGet())
                .RespondWith(Response.Create().WithSuccess().WithBody("[{\"name\":\"Lietuva\",\"distance\":\"10\"}]"));

            // act
            var result = await client.GetJson<Server[]>("servers", "token");

            // assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Lietuva", result[0].Name);
        }

        [TestMethod]
        public async Task Client_PostJson_ShouldReturnValidResult()
        {
            var client = new Client(_server.Urls.First());

            _server
                .Given(Request.Create().WithPath("/tokens").UsingPost())
                .RespondWith(Response.Create().WithSuccess().WithBody("{\"token\":\"token\"}"));

            // act
            var result = await client.PostJson<Credentials, TokenResponse>("tokens", new Credentials { Username = "user", Password = "pass" });

            // assert
            Assert.AreEqual("token", result.Token);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _server.Stop();
        }
    }
}
