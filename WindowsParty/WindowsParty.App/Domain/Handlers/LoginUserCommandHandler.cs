using Caliburn.Micro;
using Refit;
using Serilog;
using System;
using System.Threading.Tasks;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Events;
using WindowsParty.App.Services;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.Domain.Handlers
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPlaygroundClient _playgroundClient;
        private readonly ILogger _logger;

        public LoginUserCommandHandler(IEventAggregator eventAggregator, IPlaygroundClient playgroundClient, ILogger logger)
        {
            _eventAggregator = eventAggregator;
            _playgroundClient = playgroundClient;
            _logger = logger;
        }

        public async Task Handle(LoginUserCommand command)
        {
            object @event;

            try
            {
                GetTokenResponse response = await _playgroundClient.GetToken(command.Username, command.Password).ConfigureAwait(false);

                @event = new TokenSetEvent
                {
                    Token = response.Token
                };
            }
            catch (ApiException e)
            {
                @event = new LoginFailedEvent
                {
                    StatusCode = (int)e.StatusCode,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to retrieve user token");

                @event = new LoginFailedEvent
                {
                    StatusCode = -1,
                    Message = "Something went really wrong!"
                };
            }

            await _eventAggregator.PublishOnUIThreadAsync(@event).ConfigureAwait(false);
        }
    }
}
