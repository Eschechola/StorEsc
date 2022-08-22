using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IProductDomainService
{
    Task<IList<Product>> GetProductsAsync(string sellerId);
}