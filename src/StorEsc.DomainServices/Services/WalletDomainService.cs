using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class WalletDomainService : IWalletDomainService
{
    private readonly IWalletRepository _walletRepository;
    private readonly ICustomerRepository _customerRepository;
    
    public WalletDomainService(
        IWalletRepository walletRepository,
        ICustomerRepository customerRepository)
    {
        _walletRepository = walletRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Wallet> CreateNewEmptyWalletAsync()
    {
        var wallet = new Wallet(amount: 0);

        _walletRepository.Create(wallet);
        await _walletRepository.UnitOfWork.SaveChangesAsync();

        return wallet;
    }

    public async Task<Wallet> GetCustomerWalletAsync(string customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        var wallet = await _walletRepository.GetByIdAsync(customer.WalletId);
        
        return wallet;
    }

    public async Task<bool> AddAmountToWalletAsync(Guid walletId, decimal amount)
    {
        if (amount < 10)
            return false;

        var wallet = await _walletRepository.GetByIdAsync(walletId);
        
        wallet.AddAmount(amount);
        
        _walletRepository.Update(wallet);
        await _walletRepository.UnitOfWork.SaveChangesAsync();
        
        return true;
    }
}