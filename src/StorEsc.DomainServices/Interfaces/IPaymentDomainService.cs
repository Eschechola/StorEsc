using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IPaymentDomainService
{
    Task<Payment> PayRecharge(double amount, CreditCard creditCard);
}