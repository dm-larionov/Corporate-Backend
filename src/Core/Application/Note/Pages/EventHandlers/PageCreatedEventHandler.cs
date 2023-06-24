using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages.EventHandlers;

public class PageCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Page>>
{
    private readonly ILogger<PageCreatedEventHandler> _logger;

    public PageCreatedEventHandler(ILogger<PageCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Page> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered - SENDING NUDES TO ALL", @event.GetType().Name);
        return Task.CompletedTask;
    }
}