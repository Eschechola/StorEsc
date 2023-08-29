using StorEsc.Application.Dtos;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;

namespace StorEsc.Application.Interfaces;

public interface IProductApplicationService
{
    Task<Optional<ProductDto>> CreateProductAsync(string administratorId, ProductDto productDto);
    Task<IList<ProductDto>> GetLatestProductsAsync();
    Task<Optional<ProductDto>> UpdateProductAsync(string productId, string administratorId, ProductDto productDto);
    Task<bool> DisableProductAsync(string productId, string administratorId);
    Task<bool> EnableProductAsync(string productId, string administratorId);
    Task<IList<ProductDto>> SearchProductsAsync(
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending);
}
