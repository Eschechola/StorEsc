using StorEsc.Application.DTOs;
using StorEsc.Core.Data.Structs;

namespace StorEsc.Application.Interfaces;

public interface IAuthApplicationService
{
    Task<Optional<CustomerDTO>> AuthenticateCustomerAsync(string email, string password);
    Task<Optional<SellerDTO>> AuthenticateSellerAsync(string email, string password);
    Task<Optional<AdministratorDTO>> AuthenticateAdministratorAsync(string email, string password);
    Task<Optional<CustomerDTO>> RegisterCustomerAsync(CustomerDTO customerDTO);
    Task<Optional<SellerDTO>> RegisterSellerAsync(SellerDTO sellerDTO);
    Task<Optional<AdministratorDTO>> RegisterAdministratorAsync(AdministratorDTO administratorDTO);
    Task<bool> ResetCustomerPasswordAsync(string email);
    Task<bool> ResetSellerPasswordAsync(string email);
    Task<bool> ResetAdministratorPasswordAsync(string email);
}