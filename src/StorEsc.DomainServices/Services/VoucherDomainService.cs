using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class VoucherDomainService : IVoucherDomainService
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IDomainNotificationFacade _domainNotificationFacade;

    public VoucherDomainService(
        IVoucherRepository voucherRepository,
        IDomainNotificationFacade domainNotificationFacade)
    {
        _voucherRepository = voucherRepository;
        _domainNotificationFacade = domainNotificationFacade;
    }

    public async Task<Optional<Voucher>> CreateVoucherAsync(string sellerId, Voucher voucher)
    {
        voucher.Validate();

        if (voucher.IsInvalid())
        {
            await _domainNotificationFacade.PublishEntityDataIsInvalidAsync(voucher.ErrorsToString());
            return new Optional<Voucher>();
        }
        
        var exists = await _voucherRepository.ExistsAsync(entity
            => entity.Code.ToLower().Equals(voucher.Code.ToLower()));

        if (exists)
        {
            await _domainNotificationFacade.PublishAlreadyExistsAsync("Voucher");
            return new Optional<Voucher>();
        }
        
        voucher.SetSellerId(sellerId);
        voucher.CodeToUpper();
        voucher.SetDiscounts();
        voucher.Disable();

        _voucherRepository.Create(voucher);
        await _voucherRepository.UnitOfWork.SaveChangesAsync();

        return voucher;
    }

    public async Task<IList<Voucher>> GetSellerVouchersAsync(string sellerId)
        => await _voucherRepository.GetAllAsync(voucher => voucher.SellerId == Guid.Parse(sellerId));
}