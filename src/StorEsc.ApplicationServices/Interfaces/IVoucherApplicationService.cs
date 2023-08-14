using StorEsc.Application.Dtos;

namespace StorEsc.Application.Interfaces;

public interface IVoucherApplicationService
{
    Task<IList<VoucherDto>> GetSellerVouchersAsync(string sellerId);
}