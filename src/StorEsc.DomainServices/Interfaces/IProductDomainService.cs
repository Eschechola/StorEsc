using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;
using StorEsc.Domain.Entities;

namespace StorEsc.DomainServices.Interfaces;

public interface IProductDomainService
{
    Task<IList<Product>> GetLatestProductsAsync();
    Task<Optional<Product>> CreateProductAsync(Product product);
    Task<Optional<Product>> UpdateProductAsync(string productId, Product productUpdated);
    Task<bool> DisableProductAsync(string productId);
    Task<bool> EnableProductAsync(string productId);
    Task<IList<Product>> SearchProductsAsync(
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending);
}