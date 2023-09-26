using Bogus;
using FluentAssertions;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Enums;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices.Services;

public class ProductDomainServiceTests
{
    #region Properties

    private readonly IProductDomainService _sut;

    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationFacadeMock;

    private readonly ProductFaker _productFaker;

    private readonly Faker _defaultFaker;

    #endregion

    #region Constructor

    public ProductDomainServiceTests()
    {
        _defaultFaker = new Faker();

        _productFaker = new ProductFaker();

        _productRepositoryMock = new Mock<IProductRepository>();
        _domainNotificationFacadeMock = new Mock<IDomainNotificationFacade>();

        _sut = new ProductDomainService(
            productRepository: _productRepositoryMock.Object,
            domainNotificationFacade: _domainNotificationFacadeMock.Object);
    }

    #endregion

    #region GetLatestProductsAsync

    [Fact(DisplayName = "GetLatestProductsAsync when no products found returns empty list")]
    [Trait("ProductDomainService", "GetLatestProductsAsync")]
    public async Task GetLatestProductsAsync_WhenNoProductsFound_ReturnsEmptyList()
    {
        // Arrange
        var products = _productFaker.GetValidList();

        _productRepositoryMock.Setup(setup => setup.SearchProductsAsync(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                OrderBy.CreatedAtDescending))
            .ReturnsAsync(new List<Product>());

        // Act
        var result = await _sut.GetLatestProductsAsync();

        // Assert
        _productRepositoryMock.Verify(setup => setup.SearchProductsAsync(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                OrderBy.CreatedAtDescending),
            Times.Once);

        result.Should()
            .BeEmpty();
    }

    [Fact(DisplayName = "GetLatestProductsAsync when any product found returns empty list")]
    [Trait("ProductDomainService", "GetLatestProductsAsync")]
    public async Task GetLatestProductsAsync_WhenAnyProductFound_ReturnsEmptyList()
    {
        // Arrange
        var products = new List<Product>();

        _productRepositoryMock.Setup(setup => setup.SearchProductsAsync(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                OrderBy.CreatedAtDescending))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.GetLatestProductsAsync();

        // Assert
        _productRepositoryMock.Verify(setup => setup.SearchProductsAsync(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                OrderBy.CreatedAtDescending),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .BeEmpty()
            .And
            .BeEquivalentTo(products);
    }

    #endregion

    #region CreateProductAsync

    [Fact(DisplayName = "CreateProductAsync when product is not valid throw notification and returns empty optional")]
    [Trait("ProductDomainService", "CreateProductAsync")]
    public async Task CreateProductAsync_WhenProductIsNotValid_ThrowNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var product = _productFaker.GetInvalid();
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishEntityDataIsInvalidAsync(
                It.IsAny<string>()))
            .Verifiable();

        // Act
        var result = await _sut.CreateProductAsync(product);

        // Assert
        _domainNotificationFacadeMock.Verify(verify => verify.PublishEntityDataIsInvalidAsync(
                It.IsAny<string>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "CreateProductAsync when product is valid create and returns product created")]
    [Trait("ProductDomainService", "CreateProductAsync")]
    public async Task CreateProductAsync_WhenProductValid_CreateAndReturnsProductCreated()
    {
        // Arrange
        var product = _productFaker.GetValid();

        _productRepositoryMock.Setup(setup => setup.Create(product))
            .Verifiable();

        _productRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await _sut.CreateProductAsync(product);

        // Assert
        _productRepositoryMock.Verify(setup => setup.Create(product),
            Times.Once);

        _productRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once());

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(product);
    }

    #endregion

    #region UpdateProductAsync

    [Fact(DisplayName = "UpdateProductAsync when product not exists throw notification and return empty optional")]
    [Trait("ProductDomainService", "UpdateProductAsync")]
    public async Task UpdateProductAsync_WhenProductNotExists_ThrowNotificationAndReturnEmptyOptional()
    {
        // Arrange
        var product = _productFaker.GetValid();
        var productId = product.Id.ToString();
        
        _productRepositoryMock.Setup(setup =>
                setup.ExistsByIdAsync(productId))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.UpdateProductAsync(productId, product);

        // Assert
        _productRepositoryMock.Verify(verify =>
                verify.ExistsByIdAsync(productId),
            Times.Once);

        _domainNotificationFacadeMock.Verify(
            verify => verify.PublishNotFoundAsync("Product"),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "UpdateProductAsync when product data is invalid throw notification and return empty optional")]
    [Trait("ProductDomainService", "UpdateProductAsync")]
    public async Task UpdateProductAsync_WhenProductDataIsInvalid_ThrowNotificationAndReturnEmptyOptional()
    {
        // Arrange
        var product = _productFaker.GetInvalid();
        var productId = product.Id.ToString();
        
        _productRepositoryMock.Setup(setup =>
                setup.ExistsByIdAsync(productId))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(setup =>
                setup.GetByIdAsync(productId))
            .ReturnsAsync(product);

        // Act
        var result = await _sut.UpdateProductAsync(productId, product);

        // Assert
        _productRepositoryMock.Verify(verify =>
                verify.ExistsByIdAsync(productId),
            Times.Once);

        _productRepositoryMock.Verify(verify =>
                verify.GetByIdAsync(productId),
            Times.Once);

        _domainNotificationFacadeMock.Verify(
            verify => verify.PublishEntityDataIsInvalidAsync(It.IsAny<string>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "UpdateProductAsync when product is valid create and return product updated")]
    [Trait("ProductDomainService", "UpdateProductAsync")]
    public async Task UpdateProductAsync_WhenProductIsValid_CreateAndReturnProductUpdated()
    {
        // Arrange
        var product = _productFaker.GetValid();
        var productId = product.Id.ToString();

        _productRepositoryMock.Setup(setup =>
                setup.ExistsByIdAsync(productId))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(setup =>
                setup.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _productRepositoryMock.Setup(setup =>
                setup.Update(product))
            .Verifiable();

        _productRepositoryMock.Setup(setup =>
                setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await _sut.UpdateProductAsync(productId, product);

        // Assert
        _productRepositoryMock.Verify(verify =>
                verify.ExistsByIdAsync(productId),
            Times.Once);

        _productRepositoryMock.Verify(verify =>
                verify.GetByIdAsync(productId),
            Times.Once);

        _productRepositoryMock.Verify(verify =>
                verify.Update(product),
            Times.Once);

        _productRepositoryMock.Verify(verify =>
                verify.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(product);
    }

    #endregion

    #region DisableProductAsync

    [Fact(DisplayName = "DisableProductAsync when product not exists throw notification and return false")]
    [Trait("ProductDomainService", "DisableProductAsync")]
    public async Task DisableProductAsync_WhenProductNotExists_ThrowNotificationAndReturnFalse()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();

        _productRepositoryMock.Setup(setup => setup.ExistsByIdAsync(productId))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.DisableProductAsync(productId);

        // Assert
        _domainNotificationFacadeMock.Verify(verify => verify.PublishNotFoundAsync("Product"),
            Times.Once);

        result.Should()
            .BeFalse();
    }

    [Fact(DisplayName = "DisableProductAsync when product is enabled disable and return true")]
    [Trait("ProductDomainService", "DisableProductAsync")]
    public async Task DisableProductAsync_WhenProductIsEnabled_Disable_And_ReturnTrue()
    {
        // Arrange
        var product = _productFaker.GetValid();
        var productId = product.Id.ToString();

        _productRepositoryMock.Setup(setup => setup.ExistsByIdAsync(productId))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(setup => setup.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _productRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await _sut.DisableProductAsync(productId);

        // Assert
        _productRepositoryMock.Verify(verify => verify.Update(It.IsAny<Product>()),
            Times.Once);

        _productRepositoryMock.Verify(verify => verify.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);

        result.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "DisableProductAsync when product is already disabled return true")]
    [Trait("ProductDomainService", "DisableProductAsync")]
    public async Task DisableProductAsync_WhenProductIsAlreadyDisabled_ReturnTrue()
    {
        // Arrange
        var product = _productFaker.GetValid();
        var productId = product.Id.ToString();

        product.Disable();

        _productRepositoryMock.Setup(setup => setup.ExistsByIdAsync(productId))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(setup => setup.GetByIdAsync(productId))
            .ReturnsAsync(product);

        // Act
        var result = await _sut.DisableProductAsync(productId);

        // Assert
        
        
        
        result.Should()
            .BeTrue();
    }

    #endregion

    #region EnableProductAsync
    

    [Fact(DisplayName = "EnableProductAsync when product not exists throw notification and return false")]
    [Trait("ProductDomainService", "DisableProductAsync")]
    public async Task EnableProductAsync_WhenProductNotExists_ThrowNotificationAndReturnFalse()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();

        _productRepositoryMock.Setup(setup => setup.ExistsByIdAsync(productId))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.EnableProductAsync(productId);

        // Assert
        _domainNotificationFacadeMock.Verify(verify => verify.PublishNotFoundAsync("Product"),
            Times.Once);

        result.Should()
            .BeFalse();
    }

    [Fact(DisplayName = "EnableProductAsync when product is disabled enable and return true")]
    [Trait("ProductDomainService", "DisableProductAsync")]
    public async Task EnableProductAsync_WhenProductIsDisabled_EnableAndReturnTrue()
    {
        // Arrange
        var product = _productFaker.GetValid();
        var productId = product.Id.ToString();

        product.Disable();

        _productRepositoryMock.Setup(setup => setup.ExistsByIdAsync(productId))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(setup => setup.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _productRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await _sut.EnableProductAsync(productId);

        // Assert
        _productRepositoryMock.Verify(verify => verify.Update(It.IsAny<Product>()),
            Times.Once);

        _productRepositoryMock.Verify(verify => verify.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);

        result.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "EnableProductAsync when product is already enabled return true")]
    [Trait("ProductDomainService", "DisableProductAsync")]
    public async Task EnableProductAsync_WhenProductIsAlreadyEnabled_ReturnTrue()
    {
        // Arrange
        var product = _productFaker.GetValid();
        var productId = product.Id.ToString();

        _productRepositoryMock.Setup(setup => setup.ExistsByIdAsync(productId))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(setup => setup.GetByIdAsync(productId))
            .ReturnsAsync(product);

        // Act
        var result = await _sut.EnableProductAsync(productId);

        // Assert
        result.Should()
            .BeTrue();
    }

    #endregion

    #region SearchProductsAsync

    [Fact(DisplayName =
        "SearchProductsAsync when minimumPrice is lower than 0 throw notification and return empty list")]
    [Trait("ProductDomainService", "SearchProductsAsync")]
    public async Task SearchProductsAsync_WhenMinimumPriceIsLowerThanZero_ThrowNotificationAndReturnsEmptyList()
    {
        // Arrange
        var name = "test";
        var minimumPrice = -1;
        ;

        _domainNotificationFacadeMock.Setup(setup =>
                setup.PublishEntityDataIsInvalidAsync("Prices should be between 0 and 1.000.000"))
            .Verifiable();

        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice);

        // Assert
        result.Should()
            .BeEmpty();

        _domainNotificationFacadeMock.Verify(
            verify => verify.PublishEntityDataIsInvalidAsync("Prices should be between 0 and 1.000.000"),
            Times.Once);
    }

    [Fact(DisplayName =
        "SearchProductsAsync when maximumPrice is greater than 1_000_000 throw notification and return empty list")]
    [Trait("ProductDomainService", "SearchProductsAsync")]
    public async Task SearchProductsAsync_WhenMaximumPriceIsGreaterThan1kk_ThrowNotificationAndReturnsEmptyList()
    {
        // Arrange
        var name = "test";
        var minimumPrice = 5;
        var maximumPrice = 1_000_001;

        _domainNotificationFacadeMock.Setup(setup =>
                setup.PublishEntityDataIsInvalidAsync("Prices should be between 0 and 1.000.000"))
            .Verifiable();

        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice, maximumPrice);

        // Assert
        result.Should()
            .BeEmpty();

        _domainNotificationFacadeMock.Verify(
            verify => verify.PublishEntityDataIsInvalidAsync("Prices should be between 0 and 1.000.000"),
            Times.Once);
    }

    [Fact(DisplayName =
        "SearchProductsAsync when minimumPrice is greater than maximumPrice throw notification and return empty list")]
    [Trait("ProductDomainService", "SearchProductsAsync")]
    public async Task
        SearchProductsAsync_WhenMinimumPriceIsGreaterThanMaximumPrice_ThrowNotificationAndReturnsEmptyList()
    {
        // Arrange
        var name = "test";
        var minimumPrice = 100_001;
        var maximumPrice = 5;

        _domainNotificationFacadeMock.Setup(setup =>
                setup.PublishEntityDataIsInvalidAsync("Minimum price cannot be greater than maximum price"))
            .Verifiable();

        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice, maximumPrice);

        // Assert
        result.Should()
            .BeEmpty();

        _domainNotificationFacadeMock.Verify(
            verify => verify.PublishEntityDataIsInvalidAsync("Minimum price cannot be greater than maximum price"),
            Times.Once);
    }

    [Fact(DisplayName =
        "SearchProductsAsync when name lenght is greater than 200 throw notification and return empty list")]
    [Trait("ProductDomainService", "SearchProductsAsync")]
    public async Task SearchProductsAsync_WhenNameLenghtIsGreaterThan200_ThrowNotificationAndReturnsEmptyList()
    {
        // Arrange
        var name = _defaultFaker.Random.String(201);
        var minimumPrice = 5;
        var maximumPrice = 100_000;

        _domainNotificationFacadeMock.Setup(setup =>
                setup.PublishEntityDataIsInvalidAsync("Name can be between 1 and 200 characters"))
            .Verifiable();

        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice, maximumPrice);

        // Assert
        result.Should()
            .BeEmpty();

        _domainNotificationFacadeMock.Verify(
            verify => verify.PublishEntityDataIsInvalidAsync("Name can be between 1 and 200 characters"),
            Times.Once);
    }

    [Fact(DisplayName =
        "SearchProductsAsync when parameters is ok return found products")]
    [Trait("ProductDomainService", "SearchProductsAsync")]
    public async Task SearchProductsAsync_WhenParametersIsOk_ReturnFoundProducts()
    {
        // Arrange
        var name = _defaultFaker.Random.String(35);
        var minimumPrice = 5;
        var maximumPrice = 1_000_000;
        var products = _productFaker.GetValidList();

        _productRepositoryMock.Setup(setup =>
                setup.SearchProductsAsync(name, minimumPrice, maximumPrice, It.IsAny<OrderBy>()))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice, maximumPrice);

        // Assert
        result.Should()
            .BeEquivalentTo(products);

        _productRepositoryMock.Verify(
            verify => verify.SearchProductsAsync(name, minimumPrice, maximumPrice, It.IsAny<OrderBy>()),
            Times.Once);
    }

    #endregion
}