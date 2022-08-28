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

    public async Task<IList<Product>> GetProductsAsync(string sellerId)
        => await _productRepository.GetAllAsync(
            x => x.SellerId == Guid.Parse(sellerId));

    public async Task<Optional<Product>> CreateProductAsync(Product product)
    {
        try
        {
            product.Validate();

            if (!product.IsValid)
            {
                await _domainNotification.PublishProductDataIsInvalidAsync(product.ErrorsToString());
                return new Optional<Product>();
            }

            await _productRepository.UnitOfWork.BeginTransactionAsync();
            
            _productRepository.Create(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();
            
            await _productRepository.UnitOfWork.CommitAsync();

            return product;
        }
        catch (Exception)
        {
            await _productRepository.UnitOfWork.RollbackAsync();
            await _domainNotification.PublishInternalServerErrorAsync();
            return new Optional<Product>();
        }
    }
}