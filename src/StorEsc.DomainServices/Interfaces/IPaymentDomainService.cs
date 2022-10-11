using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IPaymentDomainService
{
    Task<Payment> PayRechargeAsync(decimal amount, CreditCard creditCard);
}