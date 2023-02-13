using Bogus;
using FluentAssertions;
using Moq;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices;

public class WalletDomainServiceTests
{
    #region Properties

    private readonly IWalletDomainService _sut;

    private readonly Mock<IWalletRepository> _walletRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<ISellerRepository> _sellerRepositoryMock;

    private readonly WalletFaker _walletFaker;
    private readonly SellerFaker _sellerFaker;
    private readonly CustomerFaker _customerFaker;

    private readonly Randomizer _randomizer;

    #endregion

    #region Constructor

    public WalletDomainServiceTests()
    {
        _walletRepositoryMock = new Mock<IWalletRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _sellerRepositoryMock = new Mock<ISellerRepository>();

        _walletFaker = new WalletFaker();
        _sellerFaker = new SellerFaker();
        _customerFaker = new CustomerFaker();

        _randomizer = new Randomizer();
        
        _sut = new WalletDomainService(
            walletRepository: _walletRepositoryMock.Object,
            customerRepository: _customerRepositoryMock.Object,
            sellerRepository: _sellerRepositoryMock.Object);
    }

    #endregion

    #region CreateNewEmptyWalletAsync

    [Fact(DisplayName = "CreateNewEmptyWalletAsync returns created empty wallet")]
    [Trait("WalletDomainService", "CreateNewEmptyWalletAsync")]
    public async Task CreateNewEmptyWalletAsync_ReturnsCreatedEmptyWallet()
    {
        // Arrange
        var wallet = new Wallet(amount: 0);

        _walletRepositoryMock.Setup(setup => setup.Create(It.IsAny<Wallet>()))
            .Verifiable();

        _walletRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();
        
        // Act
        var result = await _sut.CreateNewEmptyWalletAsync();

        // Assert
        _walletRepositoryMock.Verify(setup => setup.Create(It.IsAny<Wallet>()),
            Times.Once);

        _walletRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.Should()
            .BeEquivalentTo(wallet);
    }

    #endregion

    #region GetSellerWalletAsync

    [Fact(DisplayName = "GetSellerWalletAsync returns seller wallet")]
    [Trait("WalletDomainService", "GetSellerWalletAsync")]
    public async Task GetSellerWalletAsync_ReturnsSellerWallet()
    {
        // Arrange
        var sellerId = Guid.NewGuid().ToString();
        var seller = _sellerFaker.GetValid();
        var wallet = _walletFaker.GetValid();

        seller.SetWallet(wallet);

        _sellerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(sellerId),
                string.Empty,
                true))
            .ReturnsAsync(seller);
        
        _walletRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == seller.WalletId,
                string.Empty,
                true))
            .ReturnsAsync(wallet);

        // Act
        var result = await _sut.GetSellerWalletAsync(sellerId);
        
        // Assert
        _sellerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(sellerId),
                string.Empty,
                true),
            Times.Once);

        _walletRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == seller.WalletId,
                string.Empty,
                true),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.Should()
            .BeEquivalentTo(wallet);
    }    

    #endregion
    
    #region GetCustomerWalletAsync

    [Fact(DisplayName = "GetCustomerWalletAsync returns customer wallet")]
    [Trait("WalletDomainService", "GetCustomerWalletAsync")]
    public async Task GetCustomerWalletAsync_ReturnsCustomerWallet()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();
        var customer = _customerFaker.GetValid();
        var wallet = _walletFaker.GetValid();

        customer.SetWallet(wallet);

        _customerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true))
            .ReturnsAsync(customer);
        
        _walletRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == customer.WalletId,
                string.Empty,
                true))
            .ReturnsAsync(wallet);

        // Act
        var result = await _sut.GetCustomerWalletAsync(customerId);
        
        // Assert
        _customerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true),
            Times.Once);

        _walletRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == customer.WalletId,
                string.Empty,
                true),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.Should()
            .BeEquivalentTo(wallet);
    }    

    #endregion
    
    #region AddAmountToWallet

    [Fact(DisplayName = "AddAmountToWalletAsync when amount is less than ten returns false")]
    [Trait("WalletDomainService", "AddAmountToWalletAsync")]
    private async Task AddAmountToWalletAsync_WhenAmountIsLessThanTen_ReturnsFalse()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var amount = _randomizer.Decimal(0, 9);
        
        // Act
        var result = await _sut.AddAmountToWalletAsync(walletId, amount);

        // Assert 
        result.Should()
            .BeFalse();
    }

    [Fact(DisplayName = "AddAmountToWalletAsync when amount is greather than ten add amount and returns true")]
    [Trait("WalletDomainService", "AddAmountToWalletAsync")]
    private async Task AddAmountToWalletAsync_WhenAmountIsGreatherThanTen_AddAmountAndReturnsTrue()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var wallet = _walletFaker.GetValid();
        var amount = _randomizer.Decimal(10, 10_000);
        
        var expctedWalletWithAmountAdded = wallet;
        expctedWalletWithAmountAdded.AddAmount(amount);
        
        _walletRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == walletId,
                string.Empty,
                true))
            .ReturnsAsync(wallet);
        
        _walletRepositoryMock.Setup(setup => setup.Update(expctedWalletWithAmountAdded))
            .Verifiable();
        
        _walletRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();
        
        // Act
        var result = await _sut.AddAmountToWalletAsync(walletId, amount);
        
        // Assert
        _walletRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == walletId,
                string.Empty,
                true),
            Times.Once);

        _walletRepositoryMock.Verify(setup => setup.Update(expctedWalletWithAmountAdded),
            Times.Once);
        
        _walletRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        
        result.Should()
            .BeTrue();
    }
    
    #endregion
}