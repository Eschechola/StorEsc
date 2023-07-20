using StorEsc.Application.Dtos;

namespace StorEsc.Application.Interfaces;

public interface IRechargeApplicationService
{
    Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCardDto creditCardDto);
}