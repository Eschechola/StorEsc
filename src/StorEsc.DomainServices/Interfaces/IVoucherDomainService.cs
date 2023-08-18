using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IVoucherDomainService
{
    Task<Optional<Voucher>> CreateVoucherAsync(string sellerId, Voucher voucher);
    Task<IList<Voucher>> GetSellerVouchersAsync(string sellerId);
}