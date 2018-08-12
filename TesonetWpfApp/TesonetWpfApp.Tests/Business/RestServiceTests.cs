using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;
using TesonetWpfApp.Business;

namespace TesonetWpfApp.Tests.Business
{
    public class RestServiceTests
    {
        [Test]
        public void ThrowsIfResponseIsNotOk()
        {
            var mockResponse = new Mock<IRestResponse<object>>();
            mockResponse.Setup(r => r.StatusCode).Returns(System.Net.HttpStatusCode.Unauthorized);

            var restClient = new Mock<IRestClient>();
            restClient.Setup(r => r.ExecuteTaskAsync<object>(It.IsAny<IRestRequest>())).ReturnsAsync(mockResponse.Object);

            var restService = new RestService(restClient.Object);
            Assert.ThrowsAsync<RestException>(async () => await restService.Execute<object>(null, new RestRequest("TEST")));
        }

        [Test]
        public async Task ReturnsDataResponseIsOk()
        {
            var responseObject = new TestResponse();

            var mockResponse = new Mock<IRestResponse<TestResponse>>();
            mockResponse.Setup(r => r.StatusCode).Returns(System.Net.HttpStatusCode.OK);
            mockResponse.Setup(r => r.Data).Returns(responseObject);

            var restClient = new Mock<IRestClient>();
            restClient.Setup(r => r.ExecuteTaskAsync<TestResponse>(It.IsAny<IRestRequest>())).ReturnsAsync(mockResponse.Object);

            var restService = new RestService(restClient.Object);
            var actualResponse = await restService.Execute<TestResponse>(null, null);

            Assert.AreEqual(responseObject, actualResponse);
        }

        public class TestResponse
        {
            public string Test => "TEST";

            public override bool Equals(object obj)
            {
                return (obj as TestResponse).Test == Test;
            }
        }

    }
}