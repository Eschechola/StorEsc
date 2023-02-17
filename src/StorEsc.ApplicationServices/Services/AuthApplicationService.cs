using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.Application.Services;

public class AuthApplicationService : IAuthApplicationService
{
    private readonly ICustomerDomainService _customerDomainService;
    private readonly ISellerDomainService _sellerDomainService;
    private readonly IAdministratorDomainService _administratorDomainService;
    private readonly IMapper _mapper;
    
    public AuthApplicationService(
        ICustomerDomainService customerDomainService,
        ISellerDomainService sellerDomainService,
        IAdministratorDomainService administratorDomainService,
        IMapper mapper)
    {
        _customerDomainService = customerDomainService;
        _sellerDomainService = sellerDomainService;
        _administratorDomainService = administratorDomainService;
        _mapper = mapper;
    }

    #region Customer

    public async Task<Optional<CustomerDTO>> AuthenticateCustomerAsync(string email, string password)
    {
        var customer = await _customerDomainService.AuthenticateCustomerAsync(email, password);

        if (!customer.HasValue)
            return new Optional<CustomerDTO>();

        return _mapper.Map<CustomerDTO>(customer.Value);
    }

    public async Task<Optional<AdministratorDTO>> AuthenticateAdministratorAsync(string email, string password)
    {
        var administrator = await _administratorDomainService.AuthenticateAdministratorAsync(email, password);

        if (!administrator.HasValue)
            return new Optional<AdministratorDTO>();

        return _mapper.Map<AdministratorDTO>(administrator.Value);
    }

    public async Task<Optional<CustomerDTO>> RegisterCustomerAsync(CustomerDTO customerDTO)
    {
        var customer = _mapper.Map<Customer>(customerDTO);
        var customerRegistred = await _customerDomainService.RegisterCustomerAsync(customer);

        if (!customerRegistred.HasValue)
            return new Optional<CustomerDTO>();

        return _mapper.Map<CustomerDTO>(customerRegistred.Value);
    }

    public Task<Optional<AdministratorDTO>> RegisterAdministratorAsync(AdministratorDTO administratorDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetCustomerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Seller
    
    public async Task<Optional<SellerDTO>> AuthenticateSellerAsync(string email, string password)
    {
        var seller = await _sellerDomainService.AuthenticateSellerAsync(email, password);

        if (!seller.HasValue)
            return new Optional<SellerDTO>();

        return _mapper.Map<SellerDTO>(seller.Value);
    }

    public async Task<Optional<SellerDTO>> RegisterSellerAsync(SellerDTO sellerDTO)
    {
        var seller = _mapper.Map<Seller>(sellerDTO);
        var sellerRegistred = await _sellerDomainService.RegisterSellerAsync(seller);

        if (!sellerRegistred.HasValue)
            return new Optional<SellerDTO>();

        return _mapper.Map<SellerDTO>(sellerRegistred.Value);
    }

    public Task<bool> ResetSellerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetAdministratorPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }

    #endregion
}