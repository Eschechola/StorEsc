using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IRechargeDomainService
{
    Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCard creditCard);
}