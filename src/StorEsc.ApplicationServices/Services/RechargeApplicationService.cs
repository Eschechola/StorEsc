using StorEsc.Application.Dtos;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class RechargeApplicationService : IRechargeApplicationService
{
    private readonly IRechargeDomainService _rechargeDomainService;
    
    public RechargeApplicationService(IRechargeDomainService rechargeDomainService)
    {
        _rechargeDomainService = rechargeDomainService;
    }

    public async Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCardDto creditCardDto)
    {
        var creditCard = creditCardDto.AsEntity();
        return await _rechargeDomainService.RechargeCustomerWalletAsync(customerId, amount, creditCard);
    }
}