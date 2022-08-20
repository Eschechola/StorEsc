using StorEsc.Application.DTOs;
using StorEsc.Core.Data.Structs;

namespace StorEsc.Application.Interfaces;

public interface IProductApplicationService
{
    Task<Optional<IList<ProductDTO>>> GetProductsAsync(string sellerId);
}