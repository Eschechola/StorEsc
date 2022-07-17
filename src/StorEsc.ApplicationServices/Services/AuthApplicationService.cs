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
    private readonly IMapper _mapper;
    
    public AuthApplicationService(
        ICustomerDomainService customerDomainService,
        ISellerDomainService sellerDomainService,
        IMapper mapper)
    {
        _customerDomainService = customerDomainService;
        _sellerDomainService = sellerDomainService;
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

    public async Task<Optional<CustomerDTO>> RegisterCustomerAsync(CustomerDTO customerDTO)
    {
        var customer = _mapper.Map<Customer>(customerDTO);
        var customerRegistred = await _customerDomainService.RegisterCustomerAsync(customer);

        if (!customerRegistred.HasValue)
            return new Optional<CustomerDTO>();

        return _mapper.Map<CustomerDTO>(customerRegistred.Value);
    }
    
    public Task<bool> ResetCustomerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Seller
    
    public async Task<Optional<CustomerDTO>> AuthenticateSellerAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<Optional<SellerDTO>> RegisterSellerAsync(SellerDTO sellerDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetSellerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}