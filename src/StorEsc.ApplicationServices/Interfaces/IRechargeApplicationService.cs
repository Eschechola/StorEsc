using StorEsc.Application.Dtos;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IRechargeApplicationService
{
    Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCardDto creditCardDto);
}