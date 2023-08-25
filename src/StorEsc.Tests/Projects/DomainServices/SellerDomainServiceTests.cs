using Bogus;
using Bogus.DataSets;
using EscNet.Hashers.Interfaces.Algorithms;
using FluentAssertions;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices;

public class SellerDomainServiceTests
{
    #region Properties

    private readonly ISellerDomainService _sut;

    private readonly Mock<ISellerRepository> _sellerRepositoryMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationFacadeMock;
    private readonly Mock<IArgon2IdHasher> _argon2IdHasherMock;
    private readonly Mock<IWalletDomainService> _walletDomainServiceMock;

    private readonly SellerFaker _sellerFaker;
    private readonly WalletFaker _walletFaker;

    private readonly Person _personFaker;
    private readonly Internet _internetFaker;
    
    #endregion

    #region Constructor

    public SellerDomainServiceTests()
    {
        _sellerRepositoryMock = new Mock<ISellerRepository>();
        _domainNotificationFacadeMock = new Mock<IDomainNotificationFacade>();
        _argon2IdHasherMock = new Mock<IArgon2IdHasher>();
        _walletDomainServiceMock = new Mock<IWalletDomainService>();

        _sellerFaker = new SellerFaker();
        _walletFaker = new WalletFaker();

        _personFaker = new Person();
        _internetFaker = new Internet();

        _sut = new SellerDomainService(
            sellerRepository: _sellerRepositoryMock.Object,
            domainNotificationFacade: _domainNotificationFacadeMock.Object,
            argon2IdHasher: _argon2IdHasherMock.Object,
            walletDomainService: _walletDomainServiceMock.Object);
    }

    #endregion

    #region GetSellerAsync

    [Fact(DisplayName = "GetSellerAsync when seller found returns seller")]
    [Trait("SellerDomainService", "GetSellerAsync")]
    public async Task GetSellerAsync_WhenSellerFound_ReturnsSeller()
    {
        // Arrange
        var seller = _sellerFaker.GetValid();
        var sellerId = seller.Id.ToString();

        _sellerRepositoryMock.Setup(setup => setup.GetByIdAsync(sellerId))
            .ReturnsAsync(seller);

        // Act 
        var result = await _sut.GetSellerAsync(sellerId);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.GetByIdAsync(sellerId),
            Times.Once);

        result.Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(seller);
    }

    [Fact(DisplayName = "GetSellerAsync when seller not found returns null")]
    [Trait("SellerDomainService", "GetSellerAsync")]
    public async Task GetCustomerAsync_WhenCustomerNotFound_ReturnsNull()
    {
        // Arrange
        var sellerId = Guid.NewGuid().ToString();

        _sellerRepositoryMock.Setup(setup => setup.GetByIdAsync(sellerId))
            .ReturnsAsync(() => null);

        // Act 
        var result = await _sut.GetSellerAsync(sellerId);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.GetByIdAsync(sellerId),
            Times.Once);

        result.Should()
            .BeNull();
    }

    #endregion

    #region AuthenticateSellerAsync

    [Fact(DisplayName = "AuthenticateSellerAsync when email not exists throw password mismatch notification and returns empty optional")]
    [Trait("SellerDomainService", "AuthenticateSellerAsync")]
    public async Task AuthenticateSellerAsync_WhenEmailNotExists_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var email = _personFaker.Email;
        var password = _internetFaker.Password();

        _sellerRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(email))
            .ReturnsAsync(false);
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateSellerAsync(email, password);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(email),
            Times.Once);
        
        _domainNotificationFacadeMock.Verify(setup => setup.PublishEmailAndOrPasswordMismatchAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "AuthenticateSellerAsync when email exists but password is wrong throw password mismatch notification and returns empty optional")]
    [Trait("SellerDomainService", "AuthenticateSellerAsync")]
    public async Task AuthenticateSellerAsync_WhenEmailExistsButPasswordIsWrong_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var seller = _sellerFaker.GetValid();
        
        var email = seller.Email;
        var password = _internetFaker.Password();
        var hashedPassword =_internetFaker.Password(); 

        _sellerRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(email))
            .ReturnsAsync(true);
        
        _sellerRepositoryMock.Setup(setup => setup.GetByEmailAsync(email, "Wallet"))
            .ReturnsAsync(seller);

        _argon2IdHasherMock.Setup(setup => setup.Hash(password))
            .Returns(hashedPassword);
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateSellerAsync(email, password);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(email),
            Times.Once);

        _sellerRepositoryMock.Verify(setup => setup.GetByEmailAsync(email, "Wallet"),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(password),
            Times.Once);

        _domainNotificationFacadeMock.Verify(setup => setup.PublishEmailAndOrPasswordMismatchAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "AuthenticateSellerAsync when email exists and password is correct returns seller authenticated")]
    [Trait("SellerDomainService", "AuthenticateSellerAsync")]
    public async Task AuthenticateSellerAsync_WhenEmailExistsAndPasswordIsCorrect_ReturnsSellerAuthenticated()
    {
        // Arrange
        var seller = _sellerFaker.GetValid();
        var email = seller.Email;
        var password = seller.Password;
        var hashedPassword = seller.Password;
        
        _sellerRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(email))
            .ReturnsAsync(true);
        
        _sellerRepositoryMock.Setup(setup => setup.GetByEmailAsync(email, "Wallet"))
            .ReturnsAsync(seller);

        _argon2IdHasherMock.Setup(setup => setup.Hash(password))
            .Returns(hashedPassword);

        // Act
        var result = await _sut.AuthenticateSellerAsync(email, password);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(email),
            Times.Once);

        _sellerRepositoryMock.Verify(setup => setup.GetByEmailAsync(email, "Wallet"),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(password),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(seller);
    }

    #endregion

    #region RegisterSellerAsync

    [Fact(DisplayName="RegisterSellerAsync when seller already exists throw seller already exists notification and returns empty optional")]
    [Trait("SellerDomainService", "RegisterSellerAsync")]
    public async Task RegisterSellerAsync_WhenSellerAlreadyExists_ThrowSellerAlreadyExistsNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var seller = _sellerFaker.GetValid();
        
        _sellerRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(seller.Email))
            .ReturnsAsync(true);
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishAlreadyExistsAsync("Seller"))
            .Verifiable();
        
        // Act
        var result = await _sut.RegisterSellerAsync(seller);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(seller.Email),
            Times.Once);
        
        _domainNotificationFacadeMock.Verify(setup => setup.PublishAlreadyExistsAsync("Seller"),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName="RegisterSellerAsync when seller data is invalid throw seller data is invalid notification and returns empty optional")]
    [Trait("SellerDomainService", "RegisterSellerAsync")]
    public async Task RegisterSellerAsync_WhenSellerDataIsInvalid_ThrowSellerDataIsInvalidNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var seller = _sellerFaker.GetInvalid();
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishEntityDataIsInvalidAsync(It.IsAny<string>()))
            .Verifiable();
        
        // Act
        var result = await _sut.RegisterSellerAsync(seller);

        // Assert
        _domainNotificationFacadeMock.Verify(setup => setup.PublishEntityDataIsInvalidAsync(It.IsAny<string>()),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName="RegisterSellerAsync when some exception has throw made rollback, throw internal server error notification and returns empty optional")]
    [Trait("SellerDomainService", "RegisterSellerAsync")]
    public async Task RegisterSellerAsync_WhenSomeExceptionHasThrow_MadeRollbackThrowInternalServerErrorNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var wallet = _walletFaker.GetValid();
        var seller = _sellerFaker.GetValid();
        var hashedPassword = _internetFaker.Password();
        
        _sellerRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(seller.Email))
            .ReturnsAsync(false);
        
        _argon2IdHasherMock.Setup(setup => setup.Hash(seller.Password))
            .Returns(hashedPassword);

        _walletDomainServiceMock.Setup(setup => setup.CreateNewEmptyWalletAsync())
            .ReturnsAsync(wallet);

        _sellerRepositoryMock.Setup(setup => setup.UnitOfWork.BeginTransactionAsync())
            .Verifiable();
        
        _sellerRepositoryMock.Setup(setup => setup.Create(It.IsAny<Seller>()))
            .Throws(new NullReferenceException());
        
        _sellerRepositoryMock.Setup(setup => setup.UnitOfWork.RollbackAsync())
            .Verifiable();
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishInternalServerErrorAsync())
            .Verifiable();
        
        // Act
        var result = await _sut.RegisterSellerAsync(seller);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(seller.Email),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(It.IsAny<string>()),
            Times.Once);

        _walletDomainServiceMock.Verify(setup => setup.CreateNewEmptyWalletAsync(),
            Times.Once);

        _sellerRepositoryMock.Verify(setup => setup.UnitOfWork.BeginTransactionAsync(),
            Times.Once);
        
        _sellerRepositoryMock.Verify(setup => setup.Create(It.IsAny<Seller>()),
            Times.Once);
        
        _sellerRepositoryMock.Verify(setup => setup.UnitOfWork.RollbackAsync(),
            Times.Once);
        
        _domainNotificationFacadeMock.Verify(setup => setup.PublishInternalServerErrorAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "RegisterSellerAsync when seller is valid returns registered seller")]
    [Trait("SellerDomainService", "RegisterCustomerAsync")]
    public async Task RegisterSellerAsync_WhenSellerIsValid_ReturnsRegisteredSeller()
    {
        // Arrange
        var wallet = _walletFaker.GetValid();
        var seller = _sellerFaker.GetValid();
        var hashedPassword = _internetFaker.Password();

        var expectedCustomer = seller;
        expectedCustomer.SetPassword(hashedPassword);
        
        _sellerRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(seller.Email))
            .ReturnsAsync(false);
        
        _argon2IdHasherMock.Setup(setup => setup.Hash(seller.Password))
            .Returns(hashedPassword);

        _walletDomainServiceMock.Setup(setup => setup.CreateNewEmptyWalletAsync())
            .ReturnsAsync(wallet);

        _sellerRepositoryMock.Setup(setup => setup.UnitOfWork.BeginTransactionAsync())
            .Verifiable();
        
        _sellerRepositoryMock.Setup(setup => setup.Create(It.IsAny<Seller>()))
            .Verifiable();
        
        _sellerRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();
        
        _sellerRepositoryMock.Setup(setup => setup.UnitOfWork.CommitAsync())
            .Verifiable();

        // Act
        var result = await _sut.RegisterSellerAsync(seller);

        // Assert
        _sellerRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(seller.Email),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(It.IsAny<string>()),
            Times.Once);

        _walletDomainServiceMock.Verify(setup => setup.CreateNewEmptyWalletAsync(),
            Times.Once);

        _sellerRepositoryMock.Verify(setup => setup.UnitOfWork.BeginTransactionAsync(),
            Times.Once);
        
        _sellerRepositoryMock.Verify(setup => setup.Create(It.IsAny<Seller>()),
            Times.Once);
        
        _sellerRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        
        _sellerRepositoryMock.Verify(setup => setup.UnitOfWork.CommitAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(expectedCustomer);
    }

    #endregion
    
    #region ResetSellerPasswordAsync
    #endregion
}