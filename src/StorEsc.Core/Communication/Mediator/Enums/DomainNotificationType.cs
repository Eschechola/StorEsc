namespace StorEsc.Core.Communication.Mediator.Enums;

public enum DomainNotificationType
{
    CustomerDataIsInvalid,
    CustomerAlreadyExists,
    SellerDataIsInvalid,
    SellerAlreadyExists,
    ProductDataIsInvalid,
    EmailAndOrPasswordMismatch,
    InternalServerError,
    PaymentRefused,
}