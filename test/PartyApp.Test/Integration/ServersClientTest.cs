using System.Net.Http;
using System.Threading;
using PartyApp.Clients;
using PartyApp.Services;
using PartyApp.Test.Framework;
using Shouldly;
using Xunit;

namespace PartyApp.Test.Integration
{
	public class ServersClientTest
	{
		[Theory]
		[AutoMoqData]
		public async void ClientReturnsServers(ServersClient client)
		{
			var token = new AuthorizationToken("f9731b590611a5a9377fbd02f247fcdf");

			var servers = await client.GetServersAsync(token, CancellationToken.None);
			servers.ShouldNotBeEmpty();
		}

		[Theory]
		[AutoMoqData]
		public async void ClientThrowsIfResponseFails(ServersClient client)
		{
			var token = new AuthorizationToken("BadToken123");

			var exception = await Should.ThrowAsync<HttpRequestException>(
				client.GetServersAsync(token, CancellationToken.None));

			exception.Message.ShouldBe(
				"Response status code does not indicate success: 401 (Unauthorized).", 
				StringCompareShould.IgnoreCase);
		}
	}
}
