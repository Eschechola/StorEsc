using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class VoucherDomainService : IVoucherDomainService
{
    private readonly IVoucherRepository _voucherRepository;

    public VoucherDomainService(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<IList<Voucher>> GetSellerVouchersAsync(string sellerId)
        => await _voucherRepository.GetAllAsync(voucher => voucher.SellerId == Guid.Parse(sellerId));
}