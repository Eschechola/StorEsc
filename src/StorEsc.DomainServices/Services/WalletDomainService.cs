using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class WalletDomainService : IWalletDomainService
{
    private readonly IWalletRepository _walletRepository;

    public WalletDomainService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<Wallet> CreateNewEmptyWallet()
    {
        var wallet = new Wallet(amount: 0);

        _walletRepository.Create(wallet);
        await _walletRepository.UnitOfWork.SaveChangesAsync();

        return wallet;
    }
}