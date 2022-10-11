using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class WalletDomainService : IWalletDomainService
{
    private readonly IWalletRepository _walletRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ISellerRepository _sellerRepository;
    
    public WalletDomainService(
        IWalletRepository walletRepository,
        ICustomerRepository customerRepository,
        ISellerRepository sellerRepository)
    {
        _walletRepository = walletRepository;
        _customerRepository = customerRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<Wallet> CreateNewEmptyWalletAsync()
    {
        var wallet = new Wallet(amount: 0);

        _walletRepository.Create(wallet);
        await _walletRepository.UnitOfWork.SaveChangesAsync();

        return wallet;
    }

    public async Task<Wallet> GetSellerWalletAsync(string sellerId)
    {
        var seller = await _sellerRepository.GetAsync(x=>x.Id == Guid.Parse(sellerId));
        var wallet = await _walletRepository.GetAsync(x => x.Id == seller.WalletId);
        
        return wallet;
    }

    public async Task<Wallet> GetCustomerWalletAsync(string customerId)
    {
        var customer = await _customerRepository.GetAsync(x=>x.Id == Guid.Parse(customerId));
        var wallet = await _walletRepository.GetAsync(x => x.Id == customer.WalletId);
        
        return wallet;
    }

    public async Task<bool> AddAmountToWallet(Guid walletId, decimal amount)
    {
        if (amount < 10)
            return false;

        var wallet = await _walletRepository.GetAsync(
            x => x.Id == walletId);
        
        wallet.AddAmount(amount);
        
        _walletRepository.Update(wallet);
        await _walletRepository.UnitOfWork.SaveChangesAsync();
        
        return true;
    }
}