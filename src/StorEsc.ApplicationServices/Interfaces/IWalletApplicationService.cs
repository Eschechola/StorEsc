using StorEsc.Application.Dtos;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IWalletApplicationService
{
    Task<WalletDto> GetCustomerWalletAsync(string customerId);
}