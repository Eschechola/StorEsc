namespace StorEsc.Core.Communication.Mediator.Interfaces;

public interface IDomainNotificationFacade
{
    Task PublishCustomerDataIsInvalidAsync(string errors);
    Task PublishCustomerAlreadyExistsAsync();
    Task PublishEmailAndOrPasswordMismatchAsync();
}