using StorEsc.Application.Dtos;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class ProductApplicationService : IProductApplicationService
{
    private readonly IProductDomainService _productDomainService;

    public ProductApplicationService(
        IProductDomainService productDomainService)
    {
        _productDomainService = productDomainService;
    }

    public async Task<Optional<ProductDto>> CreateProductAsync(ProductDto productDto)
    {
        var product = productDto.AsEntity();
        var productCreated = await _productDomainService.CreateProductAsync(product);

        if (productCreated.IsEmpty)
            return new Optional<ProductDto>();

        return productCreated.Value.AsDto();
    }

    public async Task<IList<ProductDto>> GetLatestProductsAsync()
    {
        var products = await _productDomainService.GetLatestProductsAsync();

        return products.AsDtoList();
    }

    public async Task<IList<ProductDto>> SearchProductsAsync(
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending)
    {
        var products = await _productDomainService.SearchProductsAsync(
            name,
            minimumPrice,
            maximumPrice,
            orderBy);

        return products.AsDtoList();
    }

    public async Task<Optional<ProductDto>> UpdateProductAsync(string productId, ProductDto productDto)
    {
        var product = productDto.AsEntity();
        var productUpdated = await _productDomainService.UpdateProductAsync(productId, product);
        
        if(productUpdated.IsEmpty)
            return new Optional<ProductDto>();

        return productUpdated.Value.AsDto();
    }

    public async Task<bool> DisableProductAsync(string productId)
        => await _productDomainService.DisableProductAsync(productId);
    
    public async Task<bool> EnableProductAsync(string productId)
        => await _productDomainService.EnableProductAsync(productId);
}