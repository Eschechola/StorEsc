using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;

namespace StorEsc.Application.Interfaces;

public interface IAuthApplicationService
{
    Task<Optional<CustomerDto>> AuthenticateCustomerAsync(string email, string password);
    Task<Optional<SellerDto>> AuthenticateSellerAsync(string email, string password);
    Task<Optional<AdministratorDto>> AuthenticateAdministratorAsync(string email, string password);
    Task<Optional<CustomerDto>> RegisterCustomerAsync(CustomerDto customerDto);
    Task<Optional<SellerDto>> RegisterSellerAsync(SellerDto sellerDto);
    Task<Optional<AdministratorDto>> RegisterAdministratorAsync(AdministratorDto administratorDto);
    Task<bool> ResetCustomerPasswordAsync(string email);
    Task<bool> ResetSellerPasswordAsync(string email);
    Task<bool> ResetAdministratorPasswordAsync(string email);
}