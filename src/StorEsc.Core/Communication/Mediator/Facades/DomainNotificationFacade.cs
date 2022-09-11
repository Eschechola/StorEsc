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

    public async Task PublishCustomerDataIsInvalidAsync(string errors)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Customer data is invalid, please fix it!",
            data: errors,
            type: DomainNotificationType.CustomerDataIsInvalid));

    public async Task PublishCustomerAlreadyExistsAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Customer already exists.",
            type: DomainNotificationType.CustomerAlreadyExists));

    public async Task PublishSellerDataIsInvalidAsync(string errors)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Seller data is invalid, please fix it!",
            data: errors,
            type: DomainNotificationType.SellerDataIsInvalid));

    public async Task PublishSellerAlreadyExistsAsync()
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Seller already exists.",
            type: DomainNotificationType.SellerAlreadyExists));

    public async Task PublishProductDataIsInvalidAsync(string errors)
        => await _mediatorHandler.PublishNotificationAsync(new DomainNotification(
            message: "Product data is invalid, please fix it!",
            data: errors,
            type: DomainNotificationType.ProductDataIsInvalid));

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
}