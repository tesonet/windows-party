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
    public class ServersServiceTests
    {
        private string _serversUrl;
        private ServersService _serversService;
        private Mock<HttpMessageHandler> _handlerMock;
        private HttpResponseMessage _response;

        [TestInitialize]
        public void Setup()
        {
            _serversUrl = "http://legituri.net";

            _handlerMock = new Mock<HttpMessageHandler>();
            _response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""name"": ""Luxemburg"", ""distance"": 157 }, { ""name"": ""Germany"", ""distance"": 200 }]"),
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
            config.Setup(x => x.Get(It.IsAny<string>())).Returns(_serversUrl);

            var logger = new Mock<ILog>();

            _serversService = new ServersService(httpClient, config.Object, logger.Object);
        }

        [TestMethod]
        public async Task ShouldCallServersUrlWithGetMethodAsync()
        {
            Uri expectedUri = new Uri(_serversUrl);

            await _serversService.GetServersListAsync("token");

            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == expectedUri),
               ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task ShouldReturnServersList()
        {
            string expectedContent = await _response.Content.ReadAsStringAsync();

            HttpResponseMessage response = await _serversService.GetServersListAsync("token");
            string content = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(expectedContent, content);
        }
    }
}
