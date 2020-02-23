using AutoFixture.Xunit2;
using Caliburn.Micro;
using Moq;
using System;
using WindowsParty.App.Domain;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Events;
using WindowsParty.App.Services.Models;
using WindowsParty.App.ViewModels;
using Xunit;

namespace WindowsParty.App.UnitTests.ViewModels
{
    public class ServerViewModelTests
    {
        [Theory]
        [AutoTestData]
        public void ServerViewModel_Should_FailToStartWithNoToken(
            Mock<IEventAggregator> eventAggregator,
            Mock<ICommandProcessor> commandProcessor,
            Mock<ITokenService> tokentService)
        {
            tokentService.Setup(x => x.Token).Returns((string)null);

            Assert.Throws<ArgumentNullException>(() => new ServerViewModel(eventAggregator.Object, commandProcessor.Object, tokentService.Object));
        }

        [Theory]
        [AutoTestData]
        public void ServerViewModel_Should_OnActivePublish(
            [Frozen] Mock<IEventAggregator> eventAggregator,
            [Frozen] Mock<ICommandProcessor> commandProcessor,
            [Frozen] Mock<ITokenService> tokenService,
            string token)
        {
            tokenService.Setup(x => x.Token).Returns(token);

            var sut = new ServerViewModel(eventAggregator.Object, commandProcessor.Object, tokenService.Object);

            ScreenExtensions.TryActivate(sut);

            commandProcessor.Verify(x => x.ProcessAsync(It.IsAny<GetServersCommand>()), Times.Once);
        }

        [Theory]
        [AutoTestData]
        public void ServerViewModel_Should_UpdateServerList(
            [Frozen] Mock<IEventAggregator> eventAggregator,
            [Frozen] Mock<ICommandProcessor> commandProcessor,
            [Frozen] Mock<ITokenService> tokenService,
            string token,
            ServerListRetrievedEvent @event)
        {
            tokenService.Setup(x => x.Token).Returns(token);

            var sut = new ServerViewModel(eventAggregator.Object, commandProcessor.Object, tokenService.Object);

            sut.Handle(@event);

            Assert.Equal(@event.Servers.Length, sut.ServerList.Count);
        }
    }
}
