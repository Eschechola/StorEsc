using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IVoucherApplicationService
{
    Task<IList<VoucherDto>> GetAllVouchersAsync(string administratorId);
    Task<Optional<VoucherDto>> CreateVoucherAsync(string administratorId, VoucherDto voucherDto);
}