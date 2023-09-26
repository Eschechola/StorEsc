using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;

namespace StorEsc.ApplicationServices.Interfaces;

public interface IProductApplicationService
{
    Task<Optional<ProductDto>> CreateProductAsync(ProductDto productDto);
    Task<IList<ProductDto>> GetLatestProductsAsync();
    Task<Optional<ProductDto>> UpdateProductAsync(string productId, ProductDto productDto);
    Task<bool> DisableProductAsync(string productId);
    Task<bool> EnableProductAsync(string productId);
    Task<IList<ProductDto>> SearchProductsAsync(
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending);
}
