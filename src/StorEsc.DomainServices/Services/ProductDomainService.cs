using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;
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
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending)
    {
        if (minimumPrice < 0 || maximumPrice > 1_000_000)
        {
            await _domainNotificationFacade.PublishEntityDataIsInvalidAsync("Prices should be between 0 and 1.000.000");
            return new List<Product>();
        }

        if (minimumPrice > maximumPrice)
        {
            await _domainNotificationFacade.PublishEntityDataIsInvalidAsync(
                "Minimum price cannot be greater than maximum price");
            return new List<Product>();
        }

        if (!string.IsNullOrEmpty(name) && name.Length > 200)
        {
            await _domainNotificationFacade.PublishEntityDataIsInvalidAsync("Name can be between 1 and 200 characters");
            return new List<Product>();
        }

        return await _productRepository.SearchProductsAsync(name, minimumPrice, maximumPrice, orderBy);
    }

    public async Task<Optional<Product>> UpdateProductAsync(string productId, Product productUpdated)
    {
        var exists = await _productRepository.ExistsByIdAsync(productId);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishNotFoundAsync("Product");
            return new Optional<Product>();
        }

        var product = await _productRepository.GetByIdAsync(productId);

        product.SetName(productUpdated.Name);
        product.SetDescription(productUpdated.Description);
        product.SetPrice(productUpdated.Price);
        product.SetStock(productUpdated.Stock);

        product.Validate();

        if (product.IsInvalid())
        {
            await _domainNotificationFacade.PublishEntityDataIsInvalidAsync(product.ErrorsToString());
            return new Optional<Product>();
        }

        _productRepository.Update(product);
        await _productRepository.UnitOfWork.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DisableProductAsync(string productId)
    {
        var exists = await _productRepository.ExistsByIdAsync(productId);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishNotFoundAsync("Product");
            return false;
        }

        var product = await _productRepository.GetByIdAsync(productId);

        if (product.Enabled)
        {
            product.Disable();
            _productRepository.Update(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> EnableProductAsync(string productId)
    {
        var exists = await _productRepository.ExistsByIdAsync(productId);

        if (exists is false)
        {
            await _domainNotificationFacade.PublishNotFoundAsync("Product");
            return false;
        }

        var product = await _productRepository.GetByIdAsync(productId);

        if (product.Enabled)
            return true;

        product.Enable();
        _productRepository.Update(product);
        await _productRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<IList<Product>> GetLatestProductsAsync()
        => await SearchProductsAsync(orderBy: OrderBy.CreatedAtDescending);

    public async Task<Optional<Product>> CreateProductAsync(Product product)
    {
        product.Validate();

        if (product.IsInvalid())
        {
            await _domainNotificationFacade.PublishEntityDataIsInvalidAsync(product.ErrorsToString());
            return new Optional<Product>();
        }

        _productRepository.Create(product);
        await _productRepository.UnitOfWork.SaveChangesAsync();

        return product;
    }
}