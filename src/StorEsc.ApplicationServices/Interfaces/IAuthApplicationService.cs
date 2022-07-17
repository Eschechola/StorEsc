using StorEsc.Application.DTOs;
using StorEsc.Core.Data.Structs;

namespace StorEsc.Application.Interfaces;

public interface IAuthApplicationService
{
    Task<Optional<CustomerDTO>> AuthenticateCustomerAsync(string email, string password);
    Task<Optional<CustomerDTO>> AuthenticateSellerAsync(string email, string password);
    Task<Optional<CustomerDTO>> RegisterCustomerAsync(CustomerDTO customerDTO);
    Task<Optional<SellerDTO>> RegisterSellerAsync(SellerDTO sellerDTO);
    Task<bool> ResetCustomerPasswordAsync(string email);
    Task<bool> ResetSellerPasswordAsync(string email);
}