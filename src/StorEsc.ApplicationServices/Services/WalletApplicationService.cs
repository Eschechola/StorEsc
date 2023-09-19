using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class WalletApplicationService : IWalletApplicationService
{
    private readonly IWalletDomainService _walletDomainService;
    private readonly IMapper _mapper;

    public WalletApplicationService(
        IWalletDomainService walletDomainService, 
        IMapper mapper)
    {
        _walletDomainService = walletDomainService;
        _mapper = mapper;
    }

    public async Task<WalletDto> GetCustomerWalletAsync(string customerId)
    {
        var wallet = await _walletDomainService.GetCustomerWalletAsync(customerId);
        return _mapper.Map<WalletDto>(wallet);
    }
}