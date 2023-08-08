using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IProductDomainService
{
    Task<IList<Product>> GetLastProductsAsync();
    Task<IList<Product>> GetSellerProductsAsync(string sellerId);
    Task<Optional<Product>> CreateProductAsync(Product product);
    Task<IList<Product>> SearchProductsAsync(
        string sellerId,
        string name,
        string description,
        decimal minimumPrice = 0,
        decimal maximumPrice = Decimal.MaxValue);
    Task<Optional<Product>> UpdateProductAsync(string productId, string sellerId, Product productUpdated);
    Task<bool> DisableProductAsync(string productId, string sellerId);
    Task<bool> EnableProductAsync(string productId, string sellerId);
}