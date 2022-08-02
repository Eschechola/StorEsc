using StorEsc.Application.DTOs;
namespace StorEsc.Application.Interfaces;

public interface IWalletApplicationService
{
    Task<WalletDTO> GetSellerWallet(string sellerId);
    Task<WalletDTO> GetCustomerWallet(string customerId);
}