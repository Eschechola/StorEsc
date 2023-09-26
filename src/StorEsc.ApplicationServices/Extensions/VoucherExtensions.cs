using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class VoucherExtensions
{
    public static VoucherDto AsDto(this Voucher voucher)
        => new VoucherDto
        {
            Id = voucher.Id,
            Code = voucher.Code,
            Enabled = voucher.Enabled,
            PercentageDiscount = voucher.PercentageDiscount,
            ValueDiscount = voucher.ValueDiscount,
            IsPercentageDiscount = voucher.IsPercentageDiscount,
            CreatedAt = voucher.CreatedAt,
            UpdatedAt = voucher.UpdatedAt,
        };
    
    public static Voucher AsEntity(this VoucherDto voucherDto)
        => new Voucher(
            id: voucherDto.Id,
            code: voucherDto.Code,
            valueDiscount: voucherDto.ValueDiscount,
            percentageDiscount: voucherDto.PercentageDiscount,
            isPercentageDiscount: voucherDto.IsPercentageDiscount,
            enabled: voucherDto.Enabled,
            createdAt: voucherDto.CreatedAt,
            updatedAt: voucherDto.UpdatedAt,
            orders: null
        );

    public static IList<Voucher> AsEntityList(this IList<VoucherDto> voucherDtos)
        => voucherDtos.Select(voucher => voucher.AsEntity()).ToList();
    
    public static IList<VoucherDto> AsDtoList(this IList<Voucher> vouchers)
        => vouchers.Select(voucher => voucher.AsDto()).ToList();
}