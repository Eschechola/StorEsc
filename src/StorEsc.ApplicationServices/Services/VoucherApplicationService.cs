using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
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

    public async Task<IList<VoucherDto>> GetAllVouchersAsync(string administratorId)
    {
        var vouchers = await _voucherDomainService.GetAllVouchersAsync(administratorId);

        return _mapper.Map<IList<VoucherDto>>(vouchers);
    }

    public async Task<Optional<VoucherDto>> CreateVoucherAsync(string sellerId, VoucherDto voucherDto)
    {
        var voucher = _mapper.Map<Voucher>(voucherDto);
        var voucherCreated = await _voucherDomainService.CreateVoucherAsync(sellerId, voucher);
        
        if (voucherCreated.IsEmpty)
            return new Optional<VoucherDto>();

        return _mapper.Map<VoucherDto>(voucherCreated.Value);
    }
}