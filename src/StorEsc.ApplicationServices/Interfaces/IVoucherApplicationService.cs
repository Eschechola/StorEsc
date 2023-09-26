using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IVoucherApplicationService
{
    Task<IList<VoucherDto>> GetAllVouchersAsync();
    Task<Optional<VoucherDto>> CreateVoucherAsync(VoucherDto voucherDto);
}