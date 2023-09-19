using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class RechargeApplicationService : IRechargeApplicationService
{
    private readonly IMapper _mapper;
    private readonly IRechargeDomainService _rechargeDomainService;
    
    public RechargeApplicationService(IMapper mapper, IRechargeDomainService rechargeDomainService)
    {
        _mapper = mapper;
        _rechargeDomainService = rechargeDomainService;
    }

    public async Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCardDto creditCardDto)
    {
        var creditCard = _mapper.Map<CreditCard>(creditCardDto);
        return await _rechargeDomainService.RechargeCustomerWalletAsync(customerId, amount, creditCard);
    }
}