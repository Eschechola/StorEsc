using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface ISellerDomainService
{
    Task<Optional<Seller>> AuthenticateSellerAsync(string email, string password);
    Task<Optional<Seller>> RegisterSellerAsync(Seller seller);
    Task<bool> ResetSellerPasswordAsync(string email);
}