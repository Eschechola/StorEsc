using StorEsc.Application.Dtos;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class AuthApplicationService : IAuthApplicationService
{
    private readonly ICustomerDomainService _customerDomainService;
    private readonly IAdministratorDomainService _administratorDomainService;
    
    public AuthApplicationService(
        ICustomerDomainService customerDomainService,
        IAdministratorDomainService administratorDomainService)
    {
        _customerDomainService = customerDomainService;
        _administratorDomainService = administratorDomainService;
    }

    #region Customer

    public async Task<Optional<CustomerDto>> AuthenticateCustomerAsync(string email, string password)
    {
        var customer = await _customerDomainService.AuthenticateCustomerAsync(email, password);

        if (customer.IsEmpty)
            return new Optional<CustomerDto>();

        return customer.Value.AsDto();
    }

    public async Task<Optional<CustomerDto>> RegisterCustomerAsync(CustomerDto customerDto)
    {
        var customer = customerDto.AsEntity();
        var customerRegistered = await _customerDomainService.RegisterCustomerAsync(customer);

        if (customerRegistered.IsEmpty)
            return new Optional<CustomerDto>();

        return customerRegistered.Value.AsDto();
    }

    public Task<bool> ResetCustomerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Administrator
    
    public async Task<Optional<AdministratorDto>> AuthenticateAdministratorAsync(string email, string password)
    {
        var administrator = await _administratorDomainService.AuthenticateAdministratorAsync(email, password);

        if (administrator.IsEmpty)
            return new Optional<AdministratorDto>();

        return administrator.Value.AsDto();
    }
    
    public Task<Optional<AdministratorDto>> RegisterAdministratorAsync(string administratorId, AdministratorDto administratorDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetAdministratorPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }

    #endregion
}