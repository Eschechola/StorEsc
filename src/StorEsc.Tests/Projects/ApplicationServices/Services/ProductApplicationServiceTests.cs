using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using Moq;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.ApplicationServices.Services;
using StorEsc.Core.Data.Structs;
using StorEsc.Core.Enums;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.ApplicationServices.Services;

public class ProductApplicationServiceTests
{
    #region Properties

    private readonly IProductApplicationService _sut;

    private readonly Mock<IProductDomainService> _productDomainServiceMock;

    private readonly Commerce _commerceFaker;
    private readonly ProductFaker _productFaker;
    private readonly Randomizer _randomizerFaker;

    #endregion
    
    #region Constructor

    public ProductApplicationServiceTests()
    {
        _commerceFaker = new Commerce();
        _productFaker = new ProductFaker();
        _randomizerFaker = new Randomizer();
        
        _productDomainServiceMock = new Mock<IProductDomainService>();
        
        _sut = new ProductApplicationService(
            productDomainService: _productDomainServiceMock.Object);
    }

    #endregion

    #region CreateProductAsync

    [Fact(DisplayName = "CreateProductAsync when product not created returns empty optional")]
    [Trait("ProductApplicationService", "CreateProductAsync")]
    public async Task CreateProductAsync_WhenProductNotCreated_ReturnsEmptyOptional()
    {
        // Arrange
        var administratorId = Guid.NewGuid().ToString();
        var productDto = _productFaker.GetValid().AsDto();

        _productDomainServiceMock.Setup(setup => setup.CreateProductAsync(administratorId, It.IsAny<Product>()))
            .ReturnsAsync(new Optional<Product>());

        // Act
        var result = await _sut.CreateProductAsync(administratorId, productDto);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.CreateProductAsync(administratorId, It.IsAny<Product>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "CreateProductAsync when product has been created returns created product dto")]
    [Trait("ProductApplicationService", "CreateProductAsync")]
    public async Task CreateProductAsync_WhenProductHasBeenCreated_ReturnsCreatedProductDto()
    {
        // Arrange
        var administratorId = Guid.NewGuid().ToString();
        var productDto = _productFaker.GetValid().AsDto();

        _productDomainServiceMock.Setup(setup => setup.CreateProductAsync(administratorId, It.IsAny<Product>()))
            .ReturnsAsync(new Optional<Product>(productDto.AsEntity()));

        // Act
        var result = await _sut.CreateProductAsync(administratorId, productDto);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.CreateProductAsync(administratorId, It.IsAny<Product>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(productDto);
    }

    #endregion

    #region GetLatestProductsAsync

    [Fact(DisplayName = "GetLatestProductsAsync when products not found returns empty list")]
    [Trait("ProductApplicationService", "GetLatestProductsAsync")]
    public async Task GetLatestProductsAsync_WhenProductsNotFound_ReturnsEmptyList()
    {
        // Arrange
        _productDomainServiceMock.Setup(setup => setup.GetLatestProductsAsync())
            .ReturnsAsync(new List<Product>());
        
        // Act
        var result = await _sut.GetLatestProductsAsync();

        // Assert
        result.Should()
            .BeEmpty();
    }
    
    [Fact(DisplayName = "GetLatestProductsAsync when products has been found returns product dto list")]
    [Trait("ProductApplicationService", "GetLatestProductsAsync")]
    public async Task GetLatestProductsAsync_WhenProductsHasBeenFound_ReturnsProductDtoList()
    {
        // Arrange
        var products = _productFaker.GetValidList();
        
        _productDomainServiceMock.Setup(setup => setup.GetLatestProductsAsync())
            .ReturnsAsync(products);
        
        // Act
        var result = await _sut.GetLatestProductsAsync();

        // Assert
        result.Should()
            .NotBeEmpty();

        result.Should()
            .BeEquivalentTo(products, options => options
                .Excluding(entity => entity.OrderItens)
                .Excluding(entity => entity.Errors));
    }

    #endregion

    #region UpdateProductAsync

    [Fact(DisplayName = "UpdateProductAsync when product not created returns empty optional")]
    [Trait("ProductApplicationService", "UpdateProductAsync")]
    public async Task UpdateProductAsync_WhenProductNotCreated_ReturnsEmptyOptional()
    {
        // Arrange
        var administratorId = Guid.NewGuid().ToString();
        var productDto = _productFaker.GetValid().AsDto();
        var productId = productDto.Id.ToString();

        _productDomainServiceMock.Setup(setup => setup.UpdateProductAsync(productId, administratorId, It.IsAny<Product>()))
            .ReturnsAsync(new Optional<Product>());

        // Act
        var result = await _sut.UpdateProductAsync(productId, administratorId, productDto);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.UpdateProductAsync(productId, administratorId, It.IsAny<Product>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "UpdateProductAsync when product has been created returns created product dto")]
    [Trait("ProductApplicationService", "UpdateProductAsync")]
    public async Task UpdateProductAsync_WhenProductHasBeenCreated_ReturnsCreatedProductDto()
    {
        // Arrange
        var administratorId = Guid.NewGuid().ToString();
        var productDto = _productFaker.GetValid().AsDto();
        var productId = productDto.Id.ToString();

        _productDomainServiceMock.Setup(setup => setup.UpdateProductAsync(productId, administratorId, It.IsAny<Product>()))
            .ReturnsAsync(new Optional<Product>(productDto.AsEntity()));

        // Act
        var result = await _sut.UpdateProductAsync(productId, administratorId, productDto);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.UpdateProductAsync(productId, administratorId, It.IsAny<Product>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(productDto);
    }

    #endregion

    #region DisableProductAsync
    
    [Fact(DisplayName = "DisableProductAsync when product has been disabled returns true")]
    [Trait("ProductApplicationService", "DisableProductAsync")]
    public async Task DisableProductAsync_WhenProductHasBeenDisabled_ReturnsTrue()
    {
        // Arrange 
        var productId = Guid.NewGuid().ToString();
        var administratorId = Guid.NewGuid().ToString();

        _productDomainServiceMock.Setup(setup => setup.DisableProductAsync(productId, administratorId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _sut.DisableProductAsync(productId, administratorId);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.DisableProductAsync(productId, administratorId),
            Times.Once);
        
        result.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName = "DisableProductAsync when product can not be disabled returns false")]
    [Trait("ProductApplicationService", "DisableProductAsync")]
    public async Task DisableProductAsync_WhenProductCanNotBeDisabled_ReturnsFalse()
    {
        // Arrange 
        var productId = Guid.NewGuid().ToString();
        var administratorId = Guid.NewGuid().ToString();

        _productDomainServiceMock.Setup(setup => setup.DisableProductAsync(productId, administratorId))
            .ReturnsAsync(true);
        
        // Act
        var result = await _sut.DisableProductAsync(productId, administratorId);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.DisableProductAsync(productId, administratorId),
            Times.Once);
        
        result.Should()
            .BeTrue();
    }

    #endregion

    #region EnableProductAsync

    [Fact(DisplayName = "EnableProductAsync when product has been enabled returns true")]
    [Trait("ProductApplicationService", "EnableProductAsync")]
    public async Task EnableProductAsync_WhenProductHasBeenEnabled_ReturnsTrue()
    {
        // Arrange 
        var productId = Guid.NewGuid().ToString();
        var administratorId = Guid.NewGuid().ToString();

        _productDomainServiceMock.Setup(setup => setup.EnableProductAsync(productId, administratorId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _sut.EnableProductAsync(productId, administratorId);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.EnableProductAsync(productId, administratorId),
            Times.Once);
        
        result.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName = "EnableProductAsync when product can not be enabled returns false")]
    [Trait("ProductApplicationService", "EnableProductAsync")]
    public async Task EnableProductAsync_WhenProductCanNotBeEnabled_ReturnsFalse()
    {
        // Arrange 
        var productId = Guid.NewGuid().ToString();
        var administratorId = Guid.NewGuid().ToString();

        _productDomainServiceMock.Setup(setup => setup.EnableProductAsync(productId, administratorId))
            .ReturnsAsync(true);
        
        // Act
        var result = await _sut.EnableProductAsync(productId, administratorId);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.EnableProductAsync(productId, administratorId),
            Times.Once);
        
        result.Should()
            .BeTrue();
    }

    #endregion
    
    #region SearchProductsAsync

    [Fact(DisplayName = "SearchProductsAsync when products not found returns empty list")]
    [Trait("ProductApplicationService", "SearchProductsAsync")]
    public async Task SearchProductsAsync_WhenProductsNotFound_ReturnsEmptyList()
    {
        // Arrange
        var name = _commerceFaker.ProductName();
        var minimumPrice = _randomizerFaker.Decimal(0, 1_000_000);
        var maximumPrice = _randomizerFaker.Decimal(minimumPrice, 1_000_000);
        var orderBy = OrderBy.CreatedAtDescending;
        
        _productDomainServiceMock.Setup(setup => setup.SearchProductsAsync(
                name,
                minimumPrice,
                maximumPrice,
                orderBy))
            .ReturnsAsync(new List<Product>());
        
        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice, maximumPrice, orderBy);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.SearchProductsAsync(
                name,
                minimumPrice,
                maximumPrice,
                orderBy),
            Times.Once);

        result.Should()
            .BeEmpty();
    }
    
    [Fact(DisplayName = "SearchProductsAsync when products has been found returns product dto list")]
    [Trait("ProductApplicationService", "SearchProductsAsync")]
    public async Task SearchProductsAsync_WhenProductsHasBeenFound_ReturnsProductDtoList()
    {
        // Arrange
        var productDtos = _productFaker.GetValidList().AsDtoList();
        var name = _commerceFaker.ProductName();
        var minimumPrice = _randomizerFaker.Decimal(0, 1_000_000);
        var maximumPrice = _randomizerFaker.Decimal(minimumPrice, 1_000_000);
        var orderBy = OrderBy.CreatedAtDescending;
        
        _productDomainServiceMock.Setup(setup => setup.SearchProductsAsync(
                name,
                minimumPrice,
                maximumPrice,
                orderBy))
            .ReturnsAsync(productDtos.AsEntityList());
        
        // Act
        var result = await _sut.SearchProductsAsync(name, minimumPrice, maximumPrice, orderBy);

        // Assert
        _productDomainServiceMock.Verify(verify => verify.SearchProductsAsync(
                name,
                minimumPrice,
                maximumPrice,
                orderBy),
            Times.Once);
        
        result.Should()
            .BeEquivalentTo(productDtos);
    }

    #endregion
}