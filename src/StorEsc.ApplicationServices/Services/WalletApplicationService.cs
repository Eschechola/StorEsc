using StorEsc.Application.Dtos;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class WalletApplicationService : IWalletApplicationService
{
    private readonly IWalletDomainService _walletDomainService;

    public WalletApplicationService(
        IWalletDomainService walletDomainService)
    {
        _walletDomainService = walletDomainService;
    }

    public async Task<WalletDto> GetCustomerWalletAsync(string customerId)
    {
        var wallet = await _walletDomainService.GetCustomerWalletAsync(customerId);
        return wallet.AsDto();
    }
}