using System;
using System.Threading;
using Moq;
using PartyApp.Models;
using PartyApp.Services;
using PartyApp.Test.Framework;
using PartyApp.ViewModels;
using PartyApp.ViewModelsEvents;
using Ploeh.AutoFixture.Xunit2;
using Shouldly;
using Xunit;

namespace PartyApp.Test.Unit.ViewModels
{
	public class ServersViewModelTest
	{
		[AutoMoqData]
		[Theory]
		public async void ServersReturnFailureNotifies(
			[Frozen]Mock<IServersModel> model,
			AuthorizationToken authorizationToken,
			ServersViewModel sut,
			EventTracker<ErrorEvent, ErrorPayload> eventTracker,
			string errorMessage)
		{
			//prepare
			model.Setup(p => p.GetServersAsync(authorizationToken, It.IsAny<CancellationToken>())).
				ThrowsAsync(new InvalidOperationException(errorMessage));

			//act
			await sut.GetServersAsync(authorizationToken);

			//verify
			eventTracker.IsPublished.ShouldBeTrue();
		}

		[AutoMoqData]
		[Theory]
		public async void ServersReturnSetsServers(
			[Frozen]Mock<IServersModel> model,
			AuthorizationToken authorizationToken,
			ServersViewModel sut,
			EventTracker<ErrorEvent, ErrorPayload> eventTracker,
			string errorMessage,
			Server server)
		{
			//prepare
			model.Setup(p => p.GetServersAsync(authorizationToken, It.IsAny<CancellationToken>())).
				ReturnsAsync(new[] { server });

			//act
			await sut.GetServersAsync(authorizationToken);

			//verify
			sut.Servers.ShouldContain(server);
		}

		[AutoMoqData]
		[Theory]
		public void LogoutNotifiesNavigation(
			ServersViewModel sut,
			EventTracker<NavigationToLoginRequestedEvent, Payload> eventTracker)
		{
			//act
			sut.LogoutCommand.Execute(null);

			//verify
			eventTracker.IsPublished.ShouldBeTrue();
		}
	}
}
