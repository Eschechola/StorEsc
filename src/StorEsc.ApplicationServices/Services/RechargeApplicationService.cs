﻿using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Application.Interfaces;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.Application.Services;

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
        CreditCardDTO creditCardDTO)
    {
        var creditCard = _mapper.Map<CreditCard>(creditCardDTO);
        return await _rechargeDomainService.RechargeCustomerWalletAsync(customerId, amount, creditCard);
    }
}