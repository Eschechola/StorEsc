using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IProductDomainService
{
    Task<IList<Product>> GetLastProductsAsync();
    Task<IList<Product>> GetSellerProductsAsync(string sellerId);
    Task<Optional<Product>> CreateProductAsync(Product product);
}