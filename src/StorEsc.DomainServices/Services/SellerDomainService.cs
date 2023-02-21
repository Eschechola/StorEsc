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
    private readonly IDomainNotificationFacade _domainNotification;
    private readonly IArgon2IdHasher _argon2IdHasher;
    private readonly IWalletDomainService _walletDomainService;
    
    public SellerDomainService(
        ISellerRepository sellerRepository,
        IDomainNotificationFacade domainNotification,
        IArgon2IdHasher argon2IdHasher,
        IWalletDomainService walletDomainService)
    {
        _sellerRepository = sellerRepository;
        _domainNotification = domainNotification;
        _argon2IdHasher = argon2IdHasher;
        _walletDomainService = walletDomainService;
    }

    public async Task<Seller> GetSellerAsync(string id)
        => await _sellerRepository.GetAsync(entity => entity.Id == Guid.Parse(id));
    
    public async Task<Optional<Seller>> AuthenticateSellerAsync(string email, string password)
    {
        var exists = await _sellerRepository.ExistsAsync(
            entity => entity.Email.ToLower() == email.ToLower());

        if (exists is false)
        {
            await _domainNotification.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Seller>();
        }

        var seller = await _sellerRepository.GetAsync(
            entity => entity.Email.ToLower() == email.ToLower(),
            "Wallet");
        
        var hashedPassword = _argon2IdHasher.Hash(password);

        if (seller.Password != hashedPassword)
        {
            await _domainNotification.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Seller>();
        }
        
        return seller;
    }

    public async Task<Optional<Seller>> RegisterSellerAsync(Seller seller)
    {
        try
        {
            var exists = await _sellerRepository.ExistsAsync(
                entity => entity.Email.ToLower() == seller.Email.ToLower());

            if (exists)
            {
                await _domainNotification.PublishSellerAlreadyExistsAsync();
                return new Optional<Seller>();
            }

            seller.Validate();

            if (seller.IsInvalid())
            {
                await _domainNotification.PublishSellerDataIsInvalidAsync(seller.ErrorsToString());
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
            await _domainNotification.PublishInternalServerErrorAsync();
            return new Optional<Seller>();
        }
    }

    public Task<bool> ResetSellerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
}