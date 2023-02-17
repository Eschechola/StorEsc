using EscNet.Hashers.Interfaces.Algorithms;
using Microsoft.Extensions.Configuration;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class AdministratorDomainService : IAdministratorDomainService
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IDomainNotificationFacade _domainNotification;
    private readonly IArgon2IdHasher _argon2IdHasher;
    private readonly IConfiguration _configuration;

    public AdministratorDomainService(
        IAdministratorRepository administratorRepository,
        IDomainNotificationFacade domainNotification,
        IArgon2IdHasher argon2IdHasher,
        IConfiguration configuration)
    {
        _administratorRepository = administratorRepository;
        _domainNotification = domainNotification;
        _argon2IdHasher = argon2IdHasher;
        _configuration = configuration;
    }
    
    public async Task<Optional<Administrator>> AuthenticateAdministratorAsync(string email, string password)
    {
        var exists = await _administratorRepository.ExistsByEmailAsync(email);
        
        if (exists is false)
        {
            await _domainNotification.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Administrator>();
        }

        var administrator = await _administratorRepository.GetByEmailAsync(email);
        
        var hashedPassword = _argon2IdHasher.Hash(password);

        if (administrator.Password != hashedPassword)
        {
            await _domainNotification.PublishEmailAndOrPasswordMismatchAsync();
            return new Optional<Administrator>();
        }
        
        return administrator;
    }

    public async Task<bool> EnableDefaultAdministratorAsync()
    {
        var defaultAdministratorEmail = _configuration["Administrator:Email"];
        var administrator = await _administratorRepository.GetByEmailAsync(defaultAdministratorEmail);

        if (administrator.IsEnabled is false)
        {
            var hashedPassword = _argon2IdHasher.Hash(administrator.Password);
            administrator.SetPassword(hashedPassword);
            administrator.Enable();

            _administratorRepository.Update(administrator);
            await _administratorRepository.UnitOfWork.SaveChangesAsync();
        }

        return true;
    }


}