using EscNet.Hashers.Interfaces.Algorithms;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class SellerDomainService : ISellerDomainService
{
    private readonly ISellerRepository _sellerRepository;
    private readonly IDomainNotificationFacade _domainNotificationFacade;
    private readonly IArgon2IdHasher _argon2IdHasher;
    private readonly IWalletDomainService _walletDomainService;
    
    public SellerDomainService(
        ISellerRepository sellerRepository,
        IDomainNotificationFacade domainNotificationFacade,
        IArgon2IdHasher argon2IdHasher,
        IWalletDomainService walletDomainService)
    {
        _sellerRepository = sellerRepository;
        _domainNotificationFacade = domainNotificationFacade;
        _argon2IdHasher = argon2IdHasher;
        _walletDomainService = walletDomainService;
    }

    public async Task<Seller> GetSellerAsync(string id)
        => await _sellerRepository.GetByIdAsync(id);
    
    public async Task<Optional<Seller>> AuthenticateSellerAsync(string email, string password)
    {
        var exists = await _sellerRepository.ExistsByEmailAsync(email);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Seller>();
        }

        var seller = await _sellerRepository.GetByEmailAsync(email, "Wallet");
        
        var hashedPassword = _argon2IdHasher.Hash(password);

        if (seller.Password != hashedPassword)
        {
            await _domainNotificationFacade.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Seller>();
        }
        
        return seller;
    }

    public async Task<Optional<Seller>> RegisterSellerAsync(Seller seller)
    {
        try
        {
            var exists = await _sellerRepository.ExistsByEmailAsync(seller.Email);

            if (exists)
            {
                await _domainNotificationFacade.PublishAlreadyExistsAsync("Seller");
                return new Optional<Seller>();
            }

            seller.Validate();

            if (seller.IsInvalid())
            {
                await _domainNotificationFacade.PublishEntityDataIsInvalidAsync(seller.ErrorsToString());
                return new Optional<Seller>();
            }

            var hashedPassword = _argon2IdHasher.Hash(seller.Password);
            seller.SetPassword(hashedPassword);

            await _sellerRepository.UnitOfWork.BeginTransactionAsync();
            
            var wallet = await _walletDomainService.CreateNewEmptyWalletAsync();
            seller.SetWallet(wallet);

            _sellerRepository.Create(seller);
            await _sellerRepository.UnitOfWork.SaveChangesAsync();

            await _sellerRepository.UnitOfWork.CommitAsync();
            
            return seller;
        }
        catch (Exception)
        {
            await _sellerRepository.UnitOfWork.RollbackAsync();
            await _domainNotificationFacade.PublishInternalServerErrorAsync();
            return new Optional<Seller>();
        }
    }

    public Task<bool> ResetSellerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
}