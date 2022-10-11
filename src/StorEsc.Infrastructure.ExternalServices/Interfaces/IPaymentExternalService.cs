using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.ExternalServices.Responses;

namespace StorEsc.Infrastructure.ExternalServices.Interfaces;

public interface IPaymentExternalService
{
    Task<PaymentRechargeResponse> PayRechargeAsync(decimal amount, CreditCard creditCard);
}