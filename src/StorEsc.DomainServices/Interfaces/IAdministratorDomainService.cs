using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IAdministratorDomainService
{
    Task<Optional<Administrator>> AuthenticateAdministratorAsync(string email, string password);
    Task<bool> EnableDefaultAdministratorAsync();
}