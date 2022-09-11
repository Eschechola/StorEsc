using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IWalletDomainService
{
    Task<Wallet> CreateNewEmptyWalletAsync();
    Task<Wallet> GetSellerWalletAsync(string sellerId);
    Task<Wallet> GetCustomerWalletAsync(string customerId);
    Task<bool> AddAmountToWallet(Guid walletId, double amount);
}