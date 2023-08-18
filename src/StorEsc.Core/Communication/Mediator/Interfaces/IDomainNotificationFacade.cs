namespace StorEsc.Core.Communication.Mediator.Interfaces;

public interface IDomainNotificationFacade
{
    Task PublishEntityDataIsInvalidAsync(string errors);
    Task PublishAlreadyExistsAsync(string entityName);
    Task PublishEmailAndOrPasswordMismatchAsync();
    Task PublishInternalServerErrorAsync();
    Task PublishPaymentRefusedAsync();
    Task PublishNotFoundAsync(string entityName);
    Task PublishForbiddenAsync();
}