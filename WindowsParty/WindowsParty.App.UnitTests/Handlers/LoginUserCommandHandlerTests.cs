using AutoFixture.Xunit2;
using Caliburn.Micro;
using Moq;
using System;
using System.Threading.Tasks;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Events;
using WindowsParty.App.Domain.Handlers;
using WindowsParty.App.Services;
using WindowsParty.App.Services.Models;
using Xunit;

namespace WindowsParty.App.UnitTests.Services
{
    public class LoginUserCommandHandlerTests
    {
        [Theory]
        [AutoTestData]
        public async Task LoginUserCommandHandler_Should_CallApi(
            [Frozen] Mock<IEventAggregator> eventAggregator,
            [Frozen] Mock<IPlaygroundClient> playgroundClient,
            LoginUserCommandHandler sut,
            LoginUserCommand command,
            GetTokenResponse tokenResponse)
        {
            playgroundClient.Setup(x => x.GetToken(command.Username, command.Password)).ReturnsAsync(tokenResponse);
            SetupEventAggregator(eventAggregator);

            await sut.Handle(command);

            eventAggregator.Verify(x => x.Publish(It.Is<TokenSetEvent>(t => t.Token == tokenResponse.Token), It.IsAny<Action<System.Action>>()), Times.Once);
        }

        [Theory]
        [AutoTestData]
        public async Task LoginUserCommandHandler_Should_ReturnErrorEvent(
            [Frozen] Mock<IEventAggregator> eventAggregator,
            [Frozen] Mock<IPlaygroundClient> playgroundClient,
            LoginUserCommandHandler sut,
            LoginUserCommand command)
        {
            playgroundClient.Setup(x => x.GetToken(command.Username, command.Password)).ThrowsAsync(new Exception("test_exception"));
            SetupEventAggregator(eventAggregator);

            await sut.Handle(command);

            eventAggregator.Verify(x => x.Publish(It.Is<LoginFailedEvent>(t => t.StatusCode == -1), It.IsAny<Action<System.Action>>()), Times.Once);
        }

        private void SetupEventAggregator(Mock<IEventAggregator> eventAggregator)
        {
            eventAggregator
                .Setup(x => x.Publish(It.IsAny<object>(), It.IsAny<Action<System.Action>>()))
                .Callback((object message, Action<System.Action> marshal) => { marshal(() => { }); } );
        }

    }
}
