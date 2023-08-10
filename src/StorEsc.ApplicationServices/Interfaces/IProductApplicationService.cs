using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;

namespace StorEsc.Application.Interfaces;

public interface IProductApplicationService
{
    Task<Optional<ProductDto>> CreateProductAsync(ProductDto productDto);
    Task<IList<ProductDto>> GetLastProductsAsync();
    Task<Optional<ProductDto>> UpdateProductAsync(string productId, string sellerId, ProductDto productDto);
    Task<bool> DisableProductAsync(string productId, string sellerId);
    Task<bool> EnableProductAsync(string productId, string sellerId);
    Task<IList<ProductDto>> SearchProductsAsync(
        string sellerId = "",
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending);
}
