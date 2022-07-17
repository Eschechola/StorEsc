using StorEsc.Core.Communication.Mediator.Enums;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Core.Communication.Mediator.Handlers;

public class DomainNotificationFacade : IDomainNotificationFacade
{
    private readonly IMediatorHandler _mediatorHandler;

    public DomainNotificationFacade(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    public async Task PublishCustomerDataIsInvalidAsync(string errors)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Customer data is invalid, please fix it!",
            data: errors,
            type: DomainNotificationType.CustomerDataIsInvalid));

    public async Task PublishCustomerAlreadyExistsAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Customer already exists.",
            type: DomainNotificationType.CustomerAlreadyExists));

    public async Task PublishEmailAndOrPasswordMismatchAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Email and/or password mismatch, please try again.",
            type: DomainNotificationType.EmailAndOrPasswordMismatch));
}