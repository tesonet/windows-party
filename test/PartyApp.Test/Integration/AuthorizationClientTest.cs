using System.Threading;
using PartyApp.Clients;
using PartyApp.Properties;
using PartyApp.Services;
using PartyApp.Test.Framework;
using Shouldly;
using Xunit;

namespace PartyApp.Test.Integration
{
	public class AuthorizationClientTest
	{
		[Theory]
		[AutoMoqData]
		public async void ValidCredentialAuthorizes(AuthorizationClient client)
		{
			AuthorizationResult authorization = await client.AuthorizeAsync("tesonet", "partyanimal",
				CancellationToken.None);

			authorization.Token.Value.ShouldNotBeEmpty();
		}

		[Theory]
		[AutoMoqData]
		public async void InvalidCredentialAuthorizationThrows(AuthorizationClient client)
		{
			AuthorizationResult result = await client.AuthorizeAsync("tesonet", "sadanimal",
				CancellationToken.None);

			result.Success.ShouldBeFalse();
			result.ErrorMessage.ShouldBe(Resources.FailedToLogin);
		}
	}
}
