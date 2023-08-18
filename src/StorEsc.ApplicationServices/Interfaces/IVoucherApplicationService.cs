using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;

namespace StorEsc.Application.Interfaces;

public interface IVoucherApplicationService
{
    Task<IList<VoucherDto>> GetSellerVouchersAsync(string sellerId);
    Task<Optional<VoucherDto>> CreateVoucherAsync(string sellerId, VoucherDto voucherDto);
}