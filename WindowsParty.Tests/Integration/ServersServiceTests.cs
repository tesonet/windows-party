using Moq;
using Should;
using System.Net.Http;
using WindowsParty.Business;
using WindowsParty.Logging;
using Xunit;

namespace WindowsParty.Tests.Integration
{
	public class ServersServiceTests
	{
		private const string ValidTestUser= "tesonet";
		private const string ValidTestPassword = "partyanimal";

		private const string InvalidTestUser = "nottesonet";
		private const string InvalidTestPassword = "notpartyanimal";

		private readonly TesonetApiClient _client;

		public ServersServiceTests()
		{
			_client = new TesonetApiClient(Mock.Of<ILogger>());
		}

		[Fact]
		public async void TestValidAuthentication()
		{
			var result = await _client.AuthorizeAsync(ValidTestUser, ValidTestPassword);
			result.Success.ShouldBeTrue();
			result.Token.ShouldNotBeEmpty();
		}

		[Fact]
		public async void TestInvalidAuthentication()
		{
			var result = await _client.AuthorizeAsync(InvalidTestUser, InvalidTestPassword);
			result.Success.ShouldBeFalse();
			result.Token.ShouldBeEmpty();
		}

		[Fact]
		public async void TestAuthorizedGetServers()
		{
			var authorizeResult = await _client.AuthorizeAsync(ValidTestUser, ValidTestPassword);

			var servers = await _client.GetServersAsync(authorizeResult.Token);
			servers.ShouldNotBeEmpty();
		}

		[Fact]
		public async void TestUnAuthorizedGetServers()
		{
			await Assert.ThrowsAsync<HttpRequestException>(async () => await _client.GetServersAsync("invalid token"));
		}
	}
}
