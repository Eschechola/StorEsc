using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface ICustomerDomainService
{
    Task<Optional<Customer>> AuthenticateCustomerAsync(string email, string password);
    Task<Optional<Customer>> RegisterCustomerAsync(Customer customer);
    Task<bool> ResetCustomerPasswordAsync(string email);
}