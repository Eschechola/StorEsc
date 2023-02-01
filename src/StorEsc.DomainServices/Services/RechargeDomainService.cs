using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class RechargeDomainService : IRechargeDomainService
{
    private readonly IRechargeRepository _rechargeRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPaymentDomainService _paymentDomainService;
    private readonly IWalletDomainService _walletDomainService;
    private readonly IDomainNotificationFacade _domainNotificationFacade;
    
    public RechargeDomainService(
        IRechargeRepository rechargeRepository,
        ICustomerRepository customerRepository,
        IPaymentDomainService paymentDomainService,
        IWalletDomainService walletDomainService,
        IDomainNotificationFacade domainNotificationFacade)
    {
        _rechargeRepository = rechargeRepository;
        _customerRepository = customerRepository;
        _paymentDomainService = paymentDomainService;
        _walletDomainService = walletDomainService;
        _domainNotificationFacade = domainNotificationFacade;
    }

    public async Task<bool> RechargeCustomerWalletAsync(
        string customerId,
        decimal amount,
        CreditCard creditCard)
    {
        var payment = await _paymentDomainService.PayRechargeAsync(amount, creditCard);

        if (!payment.IsPaid)
        {
            await _domainNotificationFacade.PublishPaymentRefusedAsync();
            return false;
        }
        
        var customer = await _customerRepository.GetAsync(
            entity => entity.Id == Guid.Parse(customerId));

        var recharge = new Recharge(
            walletId: customer.WalletId,
            paymentId: payment.Id,
            amount: amount);
        
        _rechargeRepository.Create(recharge);
        await _rechargeRepository.UnitOfWork.SaveChangesAsync();

        await _walletDomainService.AddAmountToWallet(customer.WalletId, amount);
        
        return true;
    }
}