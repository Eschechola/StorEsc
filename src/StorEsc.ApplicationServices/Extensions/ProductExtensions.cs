using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class ProductExtensions
{
    public static ProductDto AsDto(this Product product)
        => new ProductDto
        {
            Id = product.Id,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Enabled = product.Enabled
        };

    public static IList<ProductDto> AsDtoList(this IList<Product> products)
        => products.Select(product => product.AsDto()).ToList();
    
    public static Product AsEntity(this ProductDto productDto)
        => new Product(
            );

    public static IList<Product> AsEntityList(this IList<ProductDto> productDtos)
        => productDtos.Select(product => product.AsEntity()).ToList();
}