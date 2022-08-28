using StorEsc.Application.DTOs;
using StorEsc.Core.Data.Structs;

namespace StorEsc.Application.Interfaces;

public interface IProductApplicationService
{
    Task<IList<ProductDTO>> GetProductsAsync(string sellerId);
    Task<Optional<ProductDTO>> CreateProductAsync(ProductDTO productDTO);
}