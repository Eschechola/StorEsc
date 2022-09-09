using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IRechargeDomainService
{
    Task<bool> RechargeCustomerWallet(
        string customerId,
        decimal amount,
        CreditCard creditCard);
}