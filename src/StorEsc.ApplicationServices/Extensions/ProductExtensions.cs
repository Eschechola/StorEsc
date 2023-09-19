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
    
    public static Product AsEntity(this ProductDto productDto)
        => new Product(
            id: productDto.Id,
            name: productDto.Name,
            description: productDto.Description,
            price: productDto.Price,
            stock: productDto.Stock,
            enabled: productDto.Enabled,
            createdAt: productDto.CreatedAt,
            updatedAt: productDto.UpdatedAt
            );

    public static IList<Product> AsEntityList(this IList<ProductDto> productDtos)
        => productDtos.Select(product => product.AsEntity()).ToList();
    
    public static IList<ProductDto> AsDtoList(this IList<Product> products)
        => products.Select(product => product.AsDto()).ToList();
}