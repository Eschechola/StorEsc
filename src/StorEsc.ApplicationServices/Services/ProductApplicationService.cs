using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Data.Structs;
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

    public async Task<IList<ProductDTO>> GetProductsAsync(string sellerId)
    {
        var products = await _productDomainService.GetProductsAsync(sellerId);

        return _mapper.Map<IList<ProductDTO>>(products);
    }
}