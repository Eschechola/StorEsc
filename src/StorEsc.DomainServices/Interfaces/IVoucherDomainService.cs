using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IVoucherDomainService
{
    Task<IList<Voucher>> GetSellerVouchersAsync(string sellerId);
}