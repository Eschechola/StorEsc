using MediatR;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Core.Communication.Mediator.Handlers;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishNotificationAsync<N>(N notification) where N : Notification
        => await _mediator.Publish(notification);
}