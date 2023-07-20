using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.Application.Services;

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

    public async Task<WalletDto> GetSellerWalletAsync(string sellerId)
    {
        var wallet = await _walletDomainService.GetSellerWalletAsync(sellerId);
        return _mapper.Map<WalletDto>(wallet);
    }

    public async Task<WalletDto> GetCustomerWalletAsync(string customerId)
    {
        var wallet = await _walletDomainService.GetCustomerWalletAsync(customerId);
        return _mapper.Map<WalletDto>(wallet);
    }
}