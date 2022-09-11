using StorEsc.Application.DTOs;

namespace StorEsc.Application.Interfaces;

public interface IRechargeApplicationService
{
    Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        double amount,
        CreditCardDTO creditCardDTO);
}