using System.Collections.Generic;
using System.Linq;
using System.Net;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Domain;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Ploeh.AutoFixture;
using RestSharp;

namespace WindowsParty.Infrastructure.Tests
{
    [TestFixture]
    public class ServerListProviderTests
    {
        private ServerListProvider _sut;
        private Mock<IRestClient> _clientMock;
        private string _token;
        private IList<Server> _expectedServers;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture();
            _token = fixture.Create<string>();
            _expectedServers = fixture.Create<IList<Server>>();

            _clientMock = new Mock<IRestClient>();
            _sut = new ServerListProvider(_clientMock.Object);
        }

        [Test]
        public void GetServers_ExecutesCorrectRestRequest()
        {
            _sut.GetServers(_token);

            _clientMock.Verify(t => t.Execute(It.Is<IRestRequest>(r =>
              r.Method == Method.GET
              && r.Resource == "servers"
              && r.Parameters.Any(p => p.Type == ParameterType.HttpHeader && p.Name == "Authorization" && (string)p.Value == $"Bearer {_token}")
            )));
        }

        [Test]
        public void GetServers_ReturnsNull_IfFailWebRequest()
        {
            _clientMock.Setup(t => t.Execute(It.IsAny<IRestRequest>())).Returns((RestResponse)null);

            var actualServers = _sut.GetServers(_token);

            Assert.IsNull(actualServers);
        }

        [Test]
        public void GetServers_ReturnsCorrectServers()
        {
            var serializedServers = JsonConvert.SerializeObject(_expectedServers);
            var restResponse = new RestResponse {Content = serializedServers, StatusCode = HttpStatusCode.OK};
            _clientMock.Setup(t => t.Execute(It.IsAny<IRestRequest>())).Returns(restResponse);

            var actualServers = _sut.GetServers(_token);

            CollectionAssert.AreEqual(_expectedServers, actualServers);
        }
    }
}
