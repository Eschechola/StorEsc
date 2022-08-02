using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IWalletDomainService
{
    Task<Wallet> CreateNewEmptyWallet();
    Task<Wallet> GetSellerWallet(string sellerId);
    Task<Wallet> GetCustomerWallet(string customerId);
}