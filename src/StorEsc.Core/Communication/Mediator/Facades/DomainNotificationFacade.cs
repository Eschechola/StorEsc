using StorEsc.Core.Communication.Mediator.Enums;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Core.Communication.Mediator.Facades;

public class DomainNotificationFacade : IDomainNotificationFacade
{
    private readonly IMediatorHandler _mediatorHandler;

    public DomainNotificationFacade(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    public async Task PublishEntityDataIsInvalidAsync(string errors)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: $"Data is invalid, please fix it!",
            data: errors,
            type: DomainNotificationType.EntityDataIsInvalid));

    public async Task PublishAlreadyExistsAsync(string entityName)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName} Customer already exists.",
            type: DomainNotificationType.AlreadyExists));

    public async Task PublishEmailAndOrPasswordMismatchAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Email and/or password mismatch, please try again.",
            type: DomainNotificationType.EmailAndOrPasswordMismatch));

    public async Task PublishInternalServerErrorAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "An internal server error has been ocurred.",
            type: DomainNotificationType.InternalServerError));

    public async Task PublishPaymentRefusedAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "The payment of recharge has been refused.",
            type: DomainNotificationType.PaymentRefused));

    public async Task PublishNotFoundAsync(string entityName)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName} not found.",
            type: DomainNotificationType.NotFound));
    
    public async Task PublishForbiddenAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "No permission",
            type: DomainNotificationType.Forbidden));

}