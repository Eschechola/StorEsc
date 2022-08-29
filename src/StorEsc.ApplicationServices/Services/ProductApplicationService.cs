using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Data.Structs;
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

    public async Task<IList<ProductDTO>> GetSellerProductsAsync(string sellerId)
    {
        var products = await _productDomainService.GetSellerProductsAsync(sellerId);

        return _mapper.Map<IList<ProductDTO>>(products);
    }

    public async Task<Optional<ProductDTO>> CreateProductAsync(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        var productCreated = await _productDomainService.CreateProductAsync(product);

        if (!productCreated.HasValue)
            return new Optional<ProductDTO>();

        return _mapper.Map<ProductDTO>(productCreated.Value);
    }

    public async Task<IList<ProductDTO>> GetLastProductsAsync()
    {
        var products = await _productDomainService.GetLastProductsAsync();

        return _mapper.Map<IList<ProductDTO>>(products);
    }
}