using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class RechargeDomainService : IRechargeDomainService
{
    private readonly IRechargeRepository _rechargeRepository;
    private readonly ICustomerRepository _customerRepository;
    
    public RechargeDomainService(IRechargeRepository rechargeRepository)
    {
        _rechargeRepository = rechargeRepository;
    }

    public async Task<bool> RechargeCustomerWallet(
        string customerId,
        decimal amount,
        CreditCard creditCard)
    {
        var customer = await _customerRepository.GetAsync(x => x.Id == Guid.Parse(customerId));
        var walletId = customer.WalletId;
        
        
        
        return true;
    }
}