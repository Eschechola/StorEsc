using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IVoucherApplicationService
{
    Task<bool> EnableVoucherAsync(string voucherId);
    Task<bool> DisableVoucherAsync(string voucherId);
    Task<IList<VoucherDto>> GetAllVouchersAsync();
    Task<Optional<VoucherDto>> CreateVoucherAsync(VoucherDto voucherDto);
    Task<Optional<VoucherDto>> UpdateVoucherAsync(string voucherId, VoucherDto voucherDto);
}