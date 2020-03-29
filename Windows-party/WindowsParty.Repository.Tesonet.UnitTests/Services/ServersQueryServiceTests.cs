namespace WindowsParty.Repository.Tesonet.UnitTests.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;
    using WindowsParty.Domain.Models;
    using WindowsParty.Repository.Tesonet.Services;

    public class ServersQueryServiceTests
    {
        private Mock<IHttpClientFactory> _httpFactoryMock;
        private Mock<HttpMessageHandler> _httpHandlerMock;
        private ServersQueryService _sut;

        [SetUp]
        public void Setup()
        {
            _httpFactoryMock = new Mock<IHttpClientFactory>();
            _httpHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_httpHandlerMock.Object);

            _httpFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(httpClient);
            var configurationMock = new Mock<IConfiguration>();
            var configurationSectionMock = new Mock<IConfigurationSection>();
            configurationSectionMock.Setup(s => s.Value).Returns("http://localhost/servers");
            configurationMock.Setup(s => s.GetSection("ServersEndpoint"))
               .Returns(configurationSectionMock.Object);

            _sut = new ServersQueryService(
                _httpFactoryMock.Object, 
                configurationMock.Object, 
                Mock.Of<ILogger<ServersQueryService>>());
        }

        [Test]
        public async Task GetAsync_GetReturnsNotSuccess_ReturnsEmptyList()
        {
            var tokenResult = new TokenResult
            {
                Token = "super-token"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound))
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.GetAsync(tokenResult);

            result.Should().BeEmpty();
            AssertRequest(request, tokenResult);
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public async Task GetAsync_NoToken_ReturnsEmptyToken(string token)
        {
            var tokenResult = new TokenResult
            {
                Token = token
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest))
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.GetAsync(tokenResult);

            result.Should().BeEmpty();
            request.Should().BeNull();
        }

        [Test]
        public async Task GetAsync_PostReturnsOK_ReturnsToken()
        {
            var tokenResult = new TokenResult
            {
                Token = "super-token"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            "[{ \"name\": \"N1\", \"distance\": 333 }, { \"name\": \"N2\", \"distance\": 446 }]")
                    })
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.GetAsync(tokenResult);

            result.Should().HaveCount(2);
            result.Should().Contain(s => (s.Name == "N1") && (s.Distance == 333));
            result.Should().Contain(s => (s.Name == "N2") && (s.Distance == 446));
            AssertRequest(request, tokenResult);
        }

        [Test]
        public async Task GetAsync_PostThrows_ReturnsEmptyToken()
        {
            var token = new TokenResult
            {
                Token = "super-token"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ThrowsAsync(new Exception())
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.GetAsync(token);

            result.Should().BeEmpty();
            AssertRequest(request, token);
        }

        private void AssertRequest(HttpRequestMessage request, TokenResult token)
        {
            request.Should().NotBeNull();
            request.Method.Should().Be(HttpMethod.Get);
            request.RequestUri.OriginalString.Should().Be("http://localhost/servers");
            request.Headers.Authorization.Should().BeEquivalentTo(new AuthenticationHeaderValue("Bearer", token.Token));
        }
    }
}