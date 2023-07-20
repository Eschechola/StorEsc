using StorEsc.Application.Dtos;
namespace StorEsc.Application.Interfaces;

public interface IWalletApplicationService
{
    Task<WalletDto> GetSellerWalletAsync(string sellerId);
    Task<WalletDto> GetCustomerWalletAsync(string customerId);
}