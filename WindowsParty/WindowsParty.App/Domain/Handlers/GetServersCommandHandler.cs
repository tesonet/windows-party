using Caliburn.Micro;
using Serilog;
using System;
using System.Threading.Tasks;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Events;
using WindowsParty.App.Services;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.Domain.Handlers
{
    public class GetServersCommandHandler : ICommandHandler<GetServersCommand>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPlaygroundClient _playgroundClient;
        private readonly ILogger _logger;

        public GetServersCommandHandler(IEventAggregator eventAggregator, IPlaygroundClient playgroundClient, ILogger logger)
        {
            _eventAggregator = eventAggregator;
            _playgroundClient = playgroundClient;
            _logger = logger;
        }

        public async Task Handle(GetServersCommand command)
        {
            object @event;

            try
            {
                GetServersResponse[] response = await _playgroundClient.GetServers(command.Token).ConfigureAwait(false);

                @event = new ServerListRetrievedEvent
                {
                    Servers = response
                };
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to retrieve server list");
                @event = new FailedToRetrieveServersEvent {};
            }

            await _eventAggregator.PublishOnUIThreadAsync(@event).ConfigureAwait(false);
        }
    }
}
