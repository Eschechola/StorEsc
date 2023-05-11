namespace StorEsc.Core.Communication.Mediator.Interfaces;

public interface IDomainNotificationFacade
{
    Task PublishCustomerDataIsInvalidAsync(string errors);
    Task PublishCustomerAlreadyExistsAsync();
    Task PublishSellerDataIsInvalidAsync(string errors);
    Task PublishSellerAlreadyExistsAsync();
    Task PublishProductDataIsInvalidAsync(string errors);
    Task PublishEmailAndOrPasswordMismatchAsync();
    Task PublishInternalServerErrorAsync();
    Task PublishPaymentRefusedAsync();
    Task PublishProductNotFoundAsync();
    Task PublishForbbidenAsync();
}