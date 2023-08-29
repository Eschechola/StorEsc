using StorEsc.Application.Dtos;

namespace StorEsc.Application.Interfaces;

public interface IWalletApplicationService
{
    Task<WalletDto> GetCustomerWalletAsync(string customerId);
}