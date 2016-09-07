using System.Linq;
using System.Net;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Domain;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using RestSharp;

namespace WindowsParty.Infrastructure.Tests
{
    [TestFixture]
    public class AuthenticatorTests
    {
        private Authenticator _sut;
        private Mock<IRestClient> _restClientMock;
        private Mock<IRestRequest> _restRequestMock;

        [SetUp]
        public void Init()
        {
            _restRequestMock = new Mock<IRestRequest>();
            _restClientMock = new Mock<IRestClient>();
            _sut = new Authenticator(_restClientMock.Object);
        }

        [Test, AutoData]
        public void Authenticate_SendsCorrectPostRequest(string username, string password)
        {
            _sut.Authenticate(username, password);

            _restClientMock.Verify(t => t.Execute(It.Is<IRestRequest>(req => req.Method == Method.POST
              && req.Resource == "tokens"
              && req.Parameters.Any(p => p.Name == "username" && (string)p.Value == username)
              && req.Parameters.Any(p => p.Name == "password" && (string)p.Value == password)
              //&& req.Parameters.Any(p => p.Type == ParameterType.HttpHeader && p.Name == "Accept" && (string)p.Value == "application/json")
              //&& req.Parameters.Any(p => p.Type == ParameterType.HttpHeader && p.Name == "Content-type" && (string)p.Value == "application/json")

            )), Times.Once);
        }

        [Test, AutoData]
        public void Authenticate_RetrievesToken_IfPostSuccessfull(string username, string password, AuthenticationResponse authenticationResponse)
        {
            var serializedAuthenticationResponse = JsonConvert.SerializeObject(authenticationResponse);
            _restClientMock
                .Setup(t => t.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse { Content = serializedAuthenticationResponse, StatusCode = HttpStatusCode.OK });

            _sut.Authenticate(username, password);

            Assert.AreEqual(authenticationResponse.Token, _sut.Token);
        }
    }
}