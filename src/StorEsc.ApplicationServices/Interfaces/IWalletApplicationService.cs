using StorEsc.Application.DTOs;
namespace StorEsc.Application.Interfaces;

public interface IWalletApplicationService
{
    Task<WalletDTO> GetSellerWalletAsync(string sellerId);
    Task<WalletDTO> GetCustomerWalletAsync(string customerId);
}