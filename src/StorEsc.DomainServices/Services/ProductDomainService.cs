using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.DomainServices.Services;

public class ProductDomainService : IProductDomainService
{
    private readonly IProductRepository _productRepository;
    private readonly IDomainNotificationFacade _domainNotification;

    public ProductDomainService(
        IProductRepository productRepository,
        IDomainNotificationFacade domainNotification)
    {
        _productRepository = productRepository;
        _domainNotification = domainNotification;
    }

    public async Task<Optional<IList<Product>>> GetProductsAsync(string sellerId)
    {
        var products = await _productRepository.GetAllAsync(
            x => x.SellerId == Guid.Parse(sellerId));

        if (!products.Any())
        {
            await _domainNotification.PublishNoProductsFoundAsync();
            return new Optional<IList<Product>>();
        }

        return new Optional<IList<Product>>(products);
    }
}