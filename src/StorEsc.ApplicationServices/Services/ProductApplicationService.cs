using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.Application.Services;

public class ProductApplicationService : IProductApplicationService
{
    private readonly IProductDomainService _productDomainService;
    private readonly IMapper _mapper;

    public ProductApplicationService(
        IProductDomainService productDomainService,
        IMapper mapper)
    {
        _productDomainService = productDomainService;
        _mapper = mapper;
    }

    public async Task<Optional<ProductDto>> CreateProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var productCreated = await _productDomainService.CreateProductAsync(product);

        if (productCreated.IsEmpty)
            return new Optional<ProductDto>();

        return _mapper.Map<ProductDto>(productCreated.Value);
    }

    public async Task<IList<ProductDto>> GetLastProductsAsync()
    {
        var products = await _productDomainService.GetLastProductsAsync();

        return _mapper.Map<IList<ProductDto>>(products);
    }

    public async Task<IList<ProductDto>> SearchProductsAsync(
        string sellerId = "",
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending)
    {
        var products = await _productDomainService.SearchProductsAsync(
            sellerId,
            name,
            minimumPrice,
            maximumPrice,
            orderBy);

        return _mapper.Map<IList<ProductDto>>(products);
    }

    public async Task<Optional<ProductDto>> UpdateProductAsync(string productId, string sellerId, ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var productUpdated = await _productDomainService.UpdateProductAsync(productId, sellerId, product);
        
        if(productUpdated.IsEmpty)
            return new Optional<ProductDto>();

        return _mapper.Map<ProductDto>(productUpdated.Value);
    }

    public async Task<bool> DisableProductAsync(string productId, string sellerId)
        => await _productDomainService.DisableProductAsync(productId, sellerId);
    
    public async Task<bool> EnableProductAsync(string productId, string sellerId)
        => await _productDomainService.EnableProductAsync(productId, sellerId);
}