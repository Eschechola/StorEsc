using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IVoucherDomainService
{
    Task<Optional<Voucher>> UpdateVoucherAsync(string voucherId, Voucher voucherUpdated);
    Task<bool> EnableVoucherAsync(string voucherId);
    Task<bool> DisableVoucherAsync(string voucherId);
    Task<Optional<Voucher>> CreateVoucherAsync(Voucher voucher);
    Task<IList<Voucher>> GetAllVouchersAsync();
}