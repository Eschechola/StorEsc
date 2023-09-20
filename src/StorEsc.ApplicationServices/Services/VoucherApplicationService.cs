using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class VoucherApplicationService : IVoucherApplicationService
{
    private readonly IVoucherDomainService _voucherDomainService;

    public VoucherApplicationService(
        IVoucherDomainService voucherDomainService)
    {
        _voucherDomainService = voucherDomainService;
    }

    public async Task<IList<VoucherDto>> GetAllVouchersAsync(string administratorId)
    {
        var vouchers = await _voucherDomainService.GetAllVouchersAsync(administratorId);

        return vouchers.AsDtoList();
    }

    public async Task<Optional<VoucherDto>> CreateVoucherAsync(string administratorId, VoucherDto voucherDto)
    {
        var voucher = voucherDto.AsEntity();
        var voucherCreated = await _voucherDomainService.CreateVoucherAsync(administratorId, voucher);
        
        if (voucherCreated.IsEmpty)
            return new Optional<VoucherDto>();

        return voucherCreated.Value.AsDto();
    }
}