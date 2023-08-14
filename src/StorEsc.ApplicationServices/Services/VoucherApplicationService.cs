using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.Application.Services;

public class VoucherApplicationService : IVoucherApplicationService
{
    private readonly IMapper _mapper;
    private readonly IVoucherDomainService _voucherDomainService;

    public VoucherApplicationService(
        IVoucherDomainService voucherDomainService,
        IMapper mapper)
    {
        _voucherDomainService = voucherDomainService;
        _mapper = mapper;
    }

    public async Task<IList<VoucherDto>> GetSellerVouchersAsync(string sellerId)
    {
        var vouchers = await _voucherDomainService.GetSellerVouchersAsync(sellerId);

        return _mapper.Map<IList<VoucherDto>>(vouchers);
    }
}