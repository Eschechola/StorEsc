﻿using EscNet.Hashers.Interfaces.Algorithms;
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
    private readonly IDomainNotificationFacade _domainNotificationFacade;

    public CustomerDomainService(
        ICustomerRepository customerRepository,
        IArgon2IdHasher argon2IdHasher,
        IWalletDomainService walletDomainService,
        IDomainNotificationFacade domainNotificationFacade)
    {
        _customerRepository = customerRepository;
        _argon2IdHasher = argon2IdHasher;
        _walletDomainService = walletDomainService;
        _domainNotificationFacade = domainNotificationFacade;
    }

    public async Task<Customer> GetCustomerAsync(string id)
        => await _customerRepository.GetByIdAsync(id);

    public async Task<Optional<Customer>> AuthenticateCustomerAsync(string email, string password)
    {
        var exists = await _customerRepository.ExistsByEmailAsync(email);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Customer>();
        }

        var customer = await _customerRepository.GetByEmailAsync(email, "Wallet");
        
        var hashedPassword = _argon2IdHasher.Hash(password);

        if (customer.Password != hashedPassword)
        {
            await _domainNotificationFacade.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Customer>();
        }
        
        return customer;
    }

    public async Task<Optional<Customer>> RegisterCustomerAsync(Customer customer)
    {
        try
        {
            customer.Validate();
            
            if (customer.IsInvalid())
            {
                await _domainNotificationFacade.PublishEntityDataIsInvalidAsync(customer.ErrorsToString());
                return new Optional<Customer>();
            }
            
            var exists = await _customerRepository.ExistsByEmailAsync(customer.Email);

            if (exists)
            {
                await _domainNotificationFacade.PublishAlreadyExistsAsync("Customer");
                return new Optional<Customer>();
            }

            customer.Validate();

            var hashedPassword = _argon2IdHasher.Hash(customer.Password);
            customer.SetPassword(hashedPassword);

            await _customerRepository.UnitOfWork.BeginTransactionAsync();
            
            var wallet = await _walletDomainService.CreateNewEmptyWalletAsync();
            customer.SetWallet(wallet);
            
            _customerRepository.Create(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync();

            await _customerRepository.UnitOfWork.CommitAsync();
            
            return customer;
        }
        catch (Exception)
        {
            await _customerRepository.UnitOfWork.RollbackAsync();
            await _domainNotificationFacade.PublishInternalServerErrorAsync();
            return new Optional<Customer>();
        }
    }

    public Task<bool> ResetCustomerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
}