using EscNet.Hashers.Interfaces.Algorithms;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class CustomerDomainService : ICustomerDomainService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IArgon2IdHasher _argon2IdHasher;
    private readonly IWalletDomainService _walletDomainService;
    private readonly IDomainNotificationFacade _domainNotification;

    public CustomerDomainService(
        ICustomerRepository customerRepository,
        IArgon2IdHasher argon2IdHasher,
        IWalletDomainService walletDomainService,
        IDomainNotificationFacade domainNotification)
    {
        _customerRepository = customerRepository;
        _argon2IdHasher = argon2IdHasher;
        _walletDomainService = walletDomainService;
        _domainNotification = domainNotification;
    }

    public async Task<Optional<Customer>> AuthenticateCustomerAsync(string email, string password)
    {
        var customerExists = await _customerRepository.ExistsAsync(x => x.Email.ToLower() == email.ToLower());

        if (!customerExists)
        {
            await _domainNotification.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Customer>();
        }

        var customer = await _customerRepository.GetAsync(x => x.Email.ToLower() == email.ToLower());
        var hashedPassword = _argon2IdHasher.Hash(password);

        if (customer.Password != hashedPassword)
        {
            await _domainNotification.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Customer>();
        }
        
        return customer;
    }

    public async Task<Optional<Customer>> RegisterCustomerAsync(Customer customer)
    {
        var customerExists = await _customerRepository.ExistsAsync(x => x.Email.ToLower() == customer.Email.ToLower());

        if (customerExists)
        {
            await _domainNotification.PublishCustomerAlreadyExistsAsync();
            return new Optional<Customer>();
        }
        
        customer.Validate();

        if (!customer.IsValid)
        {
            await _domainNotification.PublishCustomerDataIsInvalidAsync(customer.ErrorsToString());
            return new Optional<Customer>();
        }

        var hashedPassword = _argon2IdHasher.Hash(customer.Password);
        customer.SetPassword(hashedPassword);

        await _customerRepository.UnitOfWork.BeginTransactionAsync();

        var wallet = await _walletDomainService.CreateNewEmptyWallet();
        customer.SetWalletId(wallet.Id);
        
        _customerRepository.Create(customer);
        await _customerRepository.UnitOfWork.SaveChangesAsync();
        await _customerRepository.UnitOfWork.CommitAsync();

        return customer;
    }

    public Task<bool> ResetCustomerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
}