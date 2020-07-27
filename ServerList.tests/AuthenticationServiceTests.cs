using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using ServerList.Interfaces;
using ServerList.Utils;

namespace ServerList.tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private string _loginUrl;
        private string _username;
        private string _password;
        private AuthenticationService _authenticationService;
        private Mock<HttpMessageHandler> _handlerMock;
        private HttpResponseMessage _response;

        [TestInitialize]
        public void Setup()
        {
            _loginUrl = "http://legituri.net";
            _username = "username";
            _password = "password";

            _handlerMock = new Mock<HttpMessageHandler>();
            _response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""token"": ""s75urj392jfs432sdr"" }"),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(_response);

            HttpClient httpClient = new HttpClient(_handlerMock.Object);

            var config = new Mock<IConfig>();
            config.Setup(x => x.Get(It.IsAny<string>())).Returns(_loginUrl);

            var logger = new Mock<ILog>();

            _authenticationService = new AuthenticationService(httpClient, config.Object, logger.Object);
        }

        [TestMethod]
        public async Task ShouldCallLoginUrlWithPostMethodAsync()
        {
            Uri expectedUri = new Uri(_loginUrl);

            await _authenticationService.LoginAsync(_username, _password);

            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == expectedUri),
               ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task ShouldReturnToken()
        {
            string expectedContent = await _response.Content.ReadAsStringAsync();

            HttpResponseMessage response = await _authenticationService.LoginAsync(_username, _password);
            string content = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(expectedContent, content);
        }
    }
}
