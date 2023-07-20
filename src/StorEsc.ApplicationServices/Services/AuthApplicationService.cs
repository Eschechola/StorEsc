using AutoMapper;
using StorEsc.Application.Dtos;
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

    public async Task<Optional<CustomerDto>> AuthenticateCustomerAsync(string email, string password)
    {
        var customer = await _customerDomainService.AuthenticateCustomerAsync(email, password);

        if (customer.IsEmpty)
            return new Optional<CustomerDto>();

        return _mapper.Map<CustomerDto>(customer.Value);
    }

    public async Task<Optional<AdministratorDto>> AuthenticateAdministratorAsync(string email, string password)
    {
        var administrator = await _administratorDomainService.AuthenticateAdministratorAsync(email, password);

        if (administrator.IsEmpty)
            return new Optional<AdministratorDto>();

        return _mapper.Map<AdministratorDto>(administrator.Value);
    }

    public async Task<Optional<CustomerDto>> RegisterCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        var customerRegistred = await _customerDomainService.RegisterCustomerAsync(customer);

        if (customerRegistred.IsEmpty)
            return new Optional<CustomerDto>();

        return _mapper.Map<CustomerDto>(customerRegistred.Value);
    }

    public Task<Optional<AdministratorDto>> RegisterAdministratorAsync(AdministratorDto administratorDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetCustomerPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Seller
    
    public async Task<Optional<SellerDto>> AuthenticateSellerAsync(string email, string password)
    {
        var seller = await _sellerDomainService.AuthenticateSellerAsync(email, password);

        if (seller.IsEmpty)
            return new Optional<SellerDto>();

        return _mapper.Map<SellerDto>(seller.Value);
    }

    public async Task<Optional<SellerDto>> RegisterSellerAsync(SellerDto sellerDto)
    {
        var seller = _mapper.Map<Seller>(sellerDto);
        var sellerRegistred = await _sellerDomainService.RegisterSellerAsync(seller);

        if (sellerRegistred.IsEmpty)
            return new Optional<SellerDto>();

        return _mapper.Map<SellerDto>(sellerRegistred.Value);
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