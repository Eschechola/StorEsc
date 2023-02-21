using Microsoft.EntityFrameworkCore;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.EF;

namespace StorEsc.DomainServices.Services;

public class ProductDomainService : IProductDomainService
{
    private readonly IProductRepository _productRepository;
    private readonly IDomainNotificationFacade _domainNotificationFacade;

    public ProductDomainService(
        IProductRepository productRepository,
        IDomainNotificationFacade domainNotificationFacade)
    {
        _productRepository = productRepository;
        _domainNotificationFacade = domainNotificationFacade;
    }

    public async Task<IList<Product>> SearchProductsByName(string name)
        => await _productRepository.GetAllAsync(entity => Functions.FreeText(entity.Name, name));
    
    public async Task<IList<Product>> GetLastProductsAsync()
    {
        Func<IQueryable<Product>, IOrderedQueryable<Product>> orderFilter = order => order.OrderByDescending(property => property.CreatedAt);
        return await _productRepository.GetAllAsync(orderBy: orderFilter);
    }

    public async Task<IList<Product>> GetSellerProductsAsync(string sellerId)
        => await _productRepository.GetAllAsync(
            entity => entity.SellerId == Guid.Parse(sellerId));

    public async Task<Optional<Product>> CreateProductAsync(Product product)
    {
        try
        {
            product.Validate();

            if (product.IsInvalid())
            {
                await _domainNotificationFacade.PublishProductDataIsInvalidAsync(product.ErrorsToString());
                return new Optional<Product>();
            }

            _productRepository.Create(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();

            return product;
        }
        catch (Exception)
        {
            await _domainNotificationFacade.PublishInternalServerErrorAsync();
            return new Optional<Product>();
        }
    }
}