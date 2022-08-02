using AutoMapper;
using StorEsc.Application.DTOs;
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

    public async Task<WalletDTO> GetSellerWallet(string sellerId)
    {
        var wallet = await _walletDomainService.GetSellerWallet(sellerId);
        return _mapper.Map<WalletDTO>(wallet);
    }

    public async Task<WalletDTO> GetCustomerWallet(string customerId)
    {
        var wallet = await _walletDomainService.GetCustomerWallet(customerId);
        return _mapper.Map<WalletDTO>(wallet);
    }
}