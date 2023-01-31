using Bogus.DataSets;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;
using static Microsoft.EntityFrameworkCore.EF;

namespace StorEsc.Tests.Projects.DomainServices;

public class ProductDomainServiceTests
{
    #region Properties
    
    private readonly IProductDomainService _sut;

    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationFacadeMock;

    private readonly ProductFaker _productFaker;

    private readonly Commerce _commerceFaker;

    #endregion

    #region Constructor

    public ProductDomainServiceTests()
    {
        _commerceFaker = new Commerce();
        
        _productFaker = new ProductFaker();
        
        _productRepositoryMock = new Mock<IProductRepository>();
        _domainNotificationFacadeMock = new Mock<IDomainNotificationFacade>();

        _sut = new ProductDomainService(
            productRepository: _productRepositoryMock.Object,
            domainNotificationFacade: _domainNotificationFacadeMock.Object);
    }

    #endregion

    #region SearchProductsByName

    [Fact(DisplayName = "SearchProductsByName when products found returns product list")]
    [Trait("ProductDomainService", "SearchProductsByName")]
    public async Task SearchProductsByName_WhenProductsFound_ReturnsProductList()
    {
        // Arrange
        var nameToSearch = _commerceFaker.ProductName();
        var products = _productFaker.GetValidList();

        _productRepositoryMock.Setup(setup => setup.GetAllAsync(
                entity => Functions.FreeText(entity.Name, nameToSearch),
                string.Empty,
                null,
                true,
                null))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.SearchProductsByName(nameToSearch);

        // Assert
        _productRepositoryMock.Verify(setup => setup.GetAllAsync(
                entity => Functions.FreeText(entity.Name, nameToSearch),
                string.Empty,
                null,
                true,
                null),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .BeEquivalentTo(products);
    }
    
    [Fact(DisplayName = "SearchProductsByName when anyone product found returns empty list")]
    [Trait("ProductDomainService", "SearchProductsByName")]
    public async Task SearchProductsByName_WhenAnyoneProductFound_ReturnsEmptyList()
    {
        // Arrange
        var nameToSearch = _commerceFaker.ProductName();
        var products = new List<Product>();

        _productRepositoryMock.Setup(setup => setup.GetAllAsync(
                entity => Functions.FreeText(entity.Name, nameToSearch),
                string.Empty,
                null,
                true,
                null))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.SearchProductsByName(nameToSearch);

        // Assert
        _productRepositoryMock.Verify(setup => setup.GetAllAsync(
                entity => Functions.FreeText(entity.Name, nameToSearch),
                string.Empty,
                null,
                true,
                null),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .BeEmpty()
            .And
            .BeEquivalentTo(products);
    }
    
    #endregion

    #region GetLastProductsAsync

    [Fact(DisplayName = "GetLastProductsAsync when products found returns product list")]
    [Trait("ProductDomainService", "GetLastProductsAsync")]
    public async Task GetLastProductsAsync_WhenProductsFound_ReturnsProductList()
    {
        // Arrange
        var products = _productFaker.GetValidList();
        
        _productRepositoryMock.Setup(setup => setup.GetAllAsync(
                null,
                string.Empty,
                It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                true,
                null))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.GetLastProductsAsync();

        // Assert
        _productRepositoryMock.Verify(setup => setup.GetAllAsync(
                null,
                string.Empty,
                It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                true,
                null),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .BeEquivalentTo(products);
    }
    
    [Fact(DisplayName = "GetLastProductsAsync when anyone product found returns empty list")]
    [Trait("ProductDomainService", "GetLastProductsAsync")]
    public async Task GetLastProductsAsync_WhenAnyoneProductFound_ReturnsEmptyList()
    {
        // Arrange
        var products = new List<Product>();

        _productRepositoryMock.Setup(setup => setup.GetAllAsync(
                null,
                string.Empty,
                It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                true,
                null))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.GetLastProductsAsync();

        // Assert
        _productRepositoryMock.Verify(setup => setup.GetAllAsync(
                null,
                string.Empty,
                It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                true,
                null),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .BeEmpty()
            .And
            .BeEquivalentTo(products);
    }

    #endregion
    
    #region GetSellerProductsAsync

    [Fact(DisplayName = "GetSellerProductsAsync when products found returns product list")]
    [Trait("ProductDomainService", "GetSellerProductsAsync")]
    public async Task GetSellerProductsAsync_WhenProductsFound_ReturnsProductList()
    {
        // Arrange
        var sellerId = Guid.NewGuid().ToString();
        var products = _productFaker.GetValidList();

        _productRepositoryMock.Setup(setup => setup.GetAllAsync(
                entity => entity.SellerId == Guid.Parse(sellerId),
                string.Empty,
                null,
                true,
                null))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.GetSellerProductsAsync(sellerId);

        // Assert
        _productRepositoryMock.Verify(setup => setup.GetAllAsync(
                entity => entity.SellerId == Guid.Parse(sellerId),
                string.Empty,
                null,
                true,
                null),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .BeEquivalentTo(products);
    }
    
    [Fact(DisplayName = "GetSellerProductsAsync when anyone product found returns empty list")]
    [Trait("ProductDomainService", "GetSellerProductsAsync")]
    public async Task GetSellerProductsAsync_WhenAnyoneProductFound_ReturnsEmptyList()
    {
        // Arrange
        var sellerId = Guid.NewGuid().ToString();
        var products = new List<Product>();

        _productRepositoryMock.Setup(setup => setup.GetAllAsync(
                entity => entity.SellerId == Guid.Parse(sellerId),
                string.Empty,
                null,
                true,
                null))
            .ReturnsAsync(products);

        // Act
        var result = await _sut.GetSellerProductsAsync(sellerId);

        // Assert
        _productRepositoryMock.Verify(setup => setup.GetAllAsync(
                entity => entity.SellerId == Guid.Parse(sellerId),
                string.Empty,
                null,
                true,
                null),
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

        _domainNotificationFacadeMock.Setup(setup => setup.PublishProductDataIsInvalidAsync(
                It.IsAny<string>()))
            .Verifiable();

        // Act
        var result = await _sut.CreateProductAsync(product);

        // Assert
        _domainNotificationFacadeMock.Verify(setup => setup.PublishProductDataIsInvalidAsync(
                It.IsAny<string>()),
            Times.Once);

        result.HasValue.Should()
            .BeFalse();
    }

    [Fact(DisplayName =
        "CreateProductAsync when any exception has throw throw notification and returns empty optional")]
    [Trait("ProductDomainService", "CreateProductAsync")]
    public async Task CreateProductAsync_WhenAnyExceptionHasThrow_ThrowNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var product = _productFaker.GetValid();

        _productRepositoryMock.Setup(setup => setup.Create(product))
            .Throws(new Exception("Some exception!"));

        // Act
        var result = await _sut.CreateProductAsync(product);

        // Assert
        _domainNotificationFacadeMock.Verify(setup=> setup.PublishInternalServerErrorAsync(),
            Times.Once);
        
        result.HasValue.Should()
            .BeFalse();
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
        
        result.HasValue.Should()
            .BeTrue();

        result.Value.Should()
            .BeEquivalentTo(product);
    }
    
    #endregion
}