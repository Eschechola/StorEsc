using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IAuthApplicationService
{
    Task<Optional<CustomerDto>> AuthenticateCustomerAsync(string email, string password);
    Task<Optional<AdministratorDto>> AuthenticateAdministratorAsync(string email, string password);
    Task<Optional<CustomerDto>> RegisterCustomerAsync(CustomerDto customerDto);
    Task<Optional<AdministratorDto>> RegisterAdministratorAsync(string administratorId, AdministratorDto administratorDto);
    Task<bool> ResetCustomerPasswordAsync(string email);
    Task<bool> ResetAdministratorPasswordAsync(string email);
}