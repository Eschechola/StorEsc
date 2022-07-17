using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IWalletDomainService
{
    Task<Wallet> CreateNewEmptyWallet();
}