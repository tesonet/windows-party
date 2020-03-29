namespace WindowsParty.Authentication.Tesonet.UnitTests.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NUnit.Framework;
    using WindowsParty.Authentication.Tesonet.Services;
    using WindowsParty.Domain.Models;

    public class AuthenticationServiceTests
    {
        private Mock<IHttpClientFactory> _httpFactoryMock;
        private Mock<HttpMessageHandler> _httpHandlerMock;
        private AuthenticationService _sut;

        [SetUp]
        public void Setup()
        {
            _httpFactoryMock = new Mock<IHttpClientFactory>();
            _httpHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_httpHandlerMock.Object);

            _httpFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(httpClient);
            var configurationMock = new Mock<IConfiguration>();
            var configurationSectionMock = new Mock<IConfigurationSection>();
            configurationSectionMock.Setup(s => s.Value).Returns("http://localhost/token");
            configurationMock.Setup(s => s.GetSection("AuthenticationEndpoint"))
               .Returns(configurationSectionMock.Object);
            
            _sut = new AuthenticationService(
                _httpFactoryMock.Object,
                configurationMock.Object,
                Mock.Of<ILogger<AuthenticationService>>());
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public async Task LogInAsync_NoPassword_ReturnsEmptyToken(string password)
        {
            var credentials = new Credentials
            {
                Username = "someUsername",
                Password = password
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest))
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.LogInAsync(credentials);

            result.IsSuccess.Should().BeFalse();
            result.Token.Should().BeNullOrEmpty();
            request.Should().BeNull();
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public async Task LogInAsync_NoUsername_ReturnsEmptyToken(string username)
        {
            var credentials = new Credentials
            {
                Username = username,
                Password = "somePassword"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest))
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.LogInAsync(credentials);

            result.IsSuccess.Should().BeFalse();
            result.Token.Should().BeNullOrEmpty();
            request.Should().BeNull();
        }

        [Test]
        public async Task LogInAsync_PostReturnsNotSuccess_ReturnsEmptyToken()
        {
            var credentials = new Credentials
            {
                Username = "someUsername",
                Password = "somePassword"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest))
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.LogInAsync(credentials);

            result.IsSuccess.Should().BeFalse();
            result.Token.Should().BeNullOrEmpty();
            await AssertRequestAsync(request, credentials);
        }

        [Test]
        public async Task LogInAsync_PostReturnsOK_ReturnsToken()
        {
            var credentials = new Credentials
            {
                Username = "someUsername",
                Password = "somePassword"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(
                    new HttpResponseMessage(HttpStatusCode.OK)
                        { Content = new StringContent("{ \"token\": \"received\" }") })
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.LogInAsync(credentials);

            result.IsSuccess.Should().BeTrue();
            result.Token.Should().Be("received");
            await AssertRequestAsync(request, credentials);
        }

        [Test]
        public async Task LogInAsync_PostThrows_ReturnsEmptyToken()
        {
            var credentials = new Credentials
            {
                Username = "someUsername",
                Password = "somePassword"
            };
            HttpRequestMessage request = null;
            _httpHandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ThrowsAsync(new Exception())
               .Callback((HttpRequestMessage r, CancellationToken ct) => request = r);

            var result = await _sut.LogInAsync(credentials);

            result.IsSuccess.Should().BeFalse();
            result.Token.Should().BeNullOrEmpty();
            await AssertRequestAsync(request, credentials);
        }

        private async Task AssertRequestAsync(HttpRequestMessage request, Credentials credentials)
        {
            request.Should().NotBeNull();
            request.Method.Should().Be(HttpMethod.Post);
            request.RequestUri.OriginalString.Should().Be("http://localhost/token");
            var requestJson = await request.Content.ReadAsStringAsync();
            requestJson.Should()
               .Be(
                    JsonConvert.SerializeObject(
                        credentials,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
        }
    }
}