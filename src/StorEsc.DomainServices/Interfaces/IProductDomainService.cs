using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IProductDomainService
{
    Task<IList<Product>> GetLastProductsAsync();
    Task<Optional<Product>> CreateProductAsync(Product product);
    Task<Optional<Product>> UpdateProductAsync(string productId, string sellerId, Product productUpdated);
    Task<bool> DisableProductAsync(string productId, string sellerId);
    Task<bool> EnableProductAsync(string productId, string sellerId);
    Task<IList<Product>> SearchProductsAsync(
        string sellerId = "",
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending);
}