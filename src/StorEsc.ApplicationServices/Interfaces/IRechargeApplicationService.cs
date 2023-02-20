﻿using StorEsc.Application.DTOs;

namespace StorEsc.Application.Interfaces;

public interface IRechargeApplicationService
{
    Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCardDTO creditCardDTO);
}