using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

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

    public async Task<IList<Product>> SearchProductsAsync(
        string sellerId = "",
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000)
    {
        
        if (minimumPrice < 0 || maximumPrice > 1_000_000)
        {
            await _domainNotificationFacade.PublishProductDataIsInvalidAsync("Prices should be between 0 and 1.000.000");
            return new List<Product>();
        }

        if (minimumPrice > maximumPrice)
        {
            await _domainNotificationFacade.PublishProductDataIsInvalidAsync("Minimum price cannot be greater than maximum price");
            return new List<Product>();
        }

        if (!string.IsNullOrEmpty(name) && name.Length > 200)
        {
            await _domainNotificationFacade.PublishProductDataIsInvalidAsync("Name can be between 1 and 200 characters");
            return new List<Product>();
        }
        
        return await _productRepository.SearchProductsAsync(sellerId, name, minimumPrice, maximumPrice);
    }
    
    public async Task<Optional<Product>> UpdateProductAsync(string productId, string sellerId, Product productUpdated)
    {
        var exists = await _productRepository.ExistsByIdAsync(productId);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishProductNotFoundAsync();
            return new Optional<Product>();
        }

        var product = await _productRepository.GetByIdAsync(productId);

        if (product.SellerId.ToString() != sellerId)
        {
            await _domainNotificationFacade.PublishForbbidenAsync();
            return new Optional<Product>();
        }

        product.SetName(productUpdated.Name);
        product.SetDescription(productUpdated.Description);
        product.SetPrice(productUpdated.Price);
        product.SetStock(productUpdated.Stock);
        
        product.Validate();

        if (product.IsInvalid())
        {
            await _domainNotificationFacade.PublishProductDataIsInvalidAsync(product.ErrorsToString());
            return new Optional<Product>();
        }

        _productRepository.Update(product);
        await _productRepository.UnitOfWork.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DisableProductAsync(string productId, string sellerId)
    {
        var exists = await _productRepository.ExistsByIdAsync(productId);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishProductNotFoundAsync();
            return false;
        }

        var product = await _productRepository.GetByIdAsync(productId);

        if (product.SellerId.ToString() != sellerId)
        {
            await _domainNotificationFacade.PublishForbbidenAsync();
            return false;
        }

        if (product.Enabled)
        {
            product.Disable();
            _productRepository.Update(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();
        }

        return true;
    }
    
    public async Task<bool> EnableProductAsync(string productId, string sellerId)
    {
        var exists = await _productRepository.ExistsByIdAsync(productId);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishProductNotFoundAsync();
            return false;
        }

        var product = await _productRepository.GetByIdAsync(productId);

        if (product.SellerId.ToString() != sellerId)
        {
            await _domainNotificationFacade.PublishForbbidenAsync();
            return false;
        }

        if (product.Enabled)
            return true;
        
        product.Enable();
        _productRepository.Update(product);
        await _productRepository.UnitOfWork.SaveChangesAsync();
        
        return true;
    }

    public async Task<IList<Product>> GetLastProductsAsync()
    {
        Func<IQueryable<Product>, IOrderedQueryable<Product>> orderFilter = order => order.OrderByDescending(property => property.CreatedAt);
        return await _productRepository.GetAllAsync(
            entity => entity.Enabled,
            orderBy: orderFilter);
    }

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