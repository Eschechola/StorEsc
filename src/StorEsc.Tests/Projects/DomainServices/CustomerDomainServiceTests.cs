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

public class CustomerDomainServiceTests
{
    #region Properties

    private readonly ICustomerDomainService _sut;

    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IArgon2IdHasher> _argon2IdHasherMock;
    private readonly Mock<IWalletDomainService> _walletDomainServiceMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationMock;

    private readonly CustomerFaker _customerFaker;
    private readonly WalletFaker _walletFaker;
    
    private readonly Person _personFaker;
    private readonly Internet _internetFaker;
    
    #endregion

    #region Constructor

    public CustomerDomainServiceTests()
    {
        _personFaker = new Person();
        _internetFaker = new Internet();
        
        _customerFaker = new CustomerFaker();
        _walletFaker = new WalletFaker();

        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _argon2IdHasherMock = new Mock<IArgon2IdHasher>();
        _walletDomainServiceMock = new Mock<IWalletDomainService>();
        _domainNotificationMock = new Mock<IDomainNotificationFacade>();

        _sut = new CustomerDomainService(
            customerRepository: _customerRepositoryMock.Object,
            argon2IdHasher: _argon2IdHasherMock.Object,
            walletDomainService: _walletDomainServiceMock.Object,
            domainNotification: _domainNotificationMock.Object);
    }
    
    #endregion

    #region GetCustomerAsync
    
    [Fact(DisplayName = "GetCustomerAsync when customer found returns customer")]
    [Trait("CustomerDomainService", "GetCustomerAsync")]
    public async Task GetCustomerAsync_WhenCustomerFound_ReturnsCustomer()
    {
        // Arrange
        var customer = _customerFaker.GetValid();
        var customerId = customer.Id.ToString();

        _customerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true))
            .ReturnsAsync(customer);

        // Act 
        var result = await _sut.GetCustomerAsync(customerId);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true), 
            Times.Once);
        
        result.Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(customer);
    }
    
    [Fact(DisplayName = "GetCustomerAsync when customer not found returns null")]
    [Trait("CustomerDomainService", "GetCustomerAsync")]
    public async Task GetCustomerAsync_WhenCustomerNotFound_ReturnsNull()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();

        _customerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true))
            .ReturnsAsync(() => null);

        // Act 
        var result = await _sut.GetCustomerAsync(customerId);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true), 
            Times.Once);

        result.Should()
            .BeNull();
    }
    
    #endregion

    #region AuthenticateCustomerAsync

    [Fact(DisplayName = "AuthenticateCustomerAsync when email not exists throw password mismatch notification and returns empty optional")]
    [Trait("CustomerDomainService", "AuthenticateCustomerAsync")]
    public async Task AuthenticateCustomerAsync_WhenEmailNotExists_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var email = _personFaker.Email;
        var password = _internetFaker.Password();

        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == email.ToLower()))
            .ReturnsAsync(false);
        
        _domainNotificationMock.Setup(setup => setup.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == email.ToLower()),
            Times.Once);
        
        _domainNotificationMock.Verify(setup => setup.PublishEmailAndOrPasswordMismatchAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName = "AuthenticateCustomerAsync when email exists but password is wrong throw password mismatch notification and returns empty optional")]
    [Trait("CustomerDomainService", "AuthenticateCustomerAsync")]
    public async Task AuthenticateCustomerAsync_WhenEmailExistsButPasswordIsWrong_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var customer = _customerFaker.GetValid();
        
        var email = customer.Email;
        var password = _internetFaker.Password();
        var hashedPassword =_internetFaker.Password(); 

        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == email.ToLower()))
            .ReturnsAsync(true);
        
        _customerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Email.ToLower() == email.ToLower(),
                "Wallet",
                true))
            .ReturnsAsync(customer);

        _argon2IdHasherMock.Setup(setup => setup.Hash(password))
            .Returns(hashedPassword);
        
        _domainNotificationMock.Setup(setup => setup.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == email.ToLower()),
            Times.Once);

        _customerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Email.ToLower() == email.ToLower(),
                "Wallet",
                true),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(password),
            Times.Once);

        _domainNotificationMock.Verify(setup => setup.PublishEmailAndOrPasswordMismatchAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName = "AuthenticateCustomerAsync when email exists and password is correct returns customer authenticated")]
    [Trait("CustomerDomainService", "AuthenticateCustomerAsync")]
    public async Task AuthenticateCustomerAsync_WhenEmailExistsAndPasswordIsCorrect_ReturnsCustomerAuthenticated()
    {
        // Arrange
        var customer = _customerFaker.GetValid();
        var email = customer.Email;
        var password = customer.Password;
        var hashedPassword = customer.Password;
        
        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == email.ToLower()))
            .ReturnsAsync(true);
        
        _customerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Email.ToLower() == email.ToLower(),
                "Wallet",
                true))
            .ReturnsAsync(customer);

        _argon2IdHasherMock.Setup(setup => setup.Hash(password))
            .Returns(hashedPassword);

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == email.ToLower()),
            Times.Once);

        _customerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Email.ToLower() == email.ToLower(),
                "Wallet",
                true),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(password),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeTrue();

        result.Value.Should()
            .BeEquivalentTo(customer);
    }

    #endregion

    #region RegisterCustomerAsync

    [Fact(DisplayName="RegisterCustomerAsync when customer already exists throw customer already exists notification and returns empty optional")]
    [Trait("CustomerDomainService", "RegisterCustomerAsync")]
    public async Task RegisterCustomerAsync_WhenCustomerAlreadyExists_ThrowCustomerAlreadyExistsNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var customer = _customerFaker.GetValid();
        
        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()))
            .ReturnsAsync(true);
        
        _domainNotificationMock.Setup(setup => setup.PublishCustomerAlreadyExistsAsync())
            .Verifiable();
        
        // Act
        var result = await _sut.RegisterCustomerAsync(customer);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()),
            Times.Once);
        
        _domainNotificationMock.Verify(setup => setup.PublishCustomerAlreadyExistsAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName="RegisterCustomerAsync when customer data is invalid throw customer data is invalid notification and returns empty optional")]
    [Trait("CustomerDomainService", "RegisterCustomerAsync")]
    public async Task RegisterCustomerAsync_WhenCustomerDataIsInvalid_ThrowCustomerDataIsInvalidNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var customer = _customerFaker.GetInvalid();
        
        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()))
            .ReturnsAsync(false);
        
        _domainNotificationMock.Setup(setup => setup.PublishCustomerDataIsInvalidAsync(It.IsAny<string>()))
            .Verifiable();
        
        // Act
        var result = await _sut.RegisterCustomerAsync(customer);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()),
            Times.Once);
        
        _domainNotificationMock.Verify(setup => setup.PublishCustomerDataIsInvalidAsync(It.IsAny<string>()),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName="RegisterCustomerAsync when some exception has throw made rollback, throw internal server error notification and returns empty optional")]
    [Trait("CustomerDomainService", "RegisterCustomerAsync")]
    public async Task RegisterCustomerAsync_WhenSomeExceptionHasThrow_MadeRollbackThrowInternalServerErrorNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var wallet = _walletFaker.GetValid();
        var customer = _customerFaker.GetValid();
        var hashedPassword = _internetFaker.Password();
        
        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()))
            .ReturnsAsync(false);
        
        _argon2IdHasherMock.Setup(setup => setup.Hash(customer.Password))
            .Returns(hashedPassword);

        _walletDomainServiceMock.Setup(setup => setup.CreateNewEmptyWalletAsync())
            .ReturnsAsync(wallet);

        _customerRepositoryMock.Setup(setup => setup.UnitOfWork.BeginTransactionAsync())
            .Verifiable();
        
        _customerRepositoryMock.Setup(setup => setup.Create(It.IsAny<Customer>()))
            .Throws(new NullReferenceException());
        
        _customerRepositoryMock.Setup(setup => setup.UnitOfWork.RollbackAsync())
            .Verifiable();
        
        _domainNotificationMock.Setup(setup => setup.PublishInternalServerErrorAsync())
            .Verifiable();
        
        // Act
        var result = await _sut.RegisterCustomerAsync(customer);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(It.IsAny<string>()),
            Times.Once);

        _walletDomainServiceMock.Verify(setup => setup.CreateNewEmptyWalletAsync(),
            Times.Once);

        _customerRepositoryMock.Verify(setup => setup.UnitOfWork.BeginTransactionAsync(),
            Times.Once);
        
        _customerRepositoryMock.Verify(setup => setup.Create(It.IsAny<Customer>()),
            Times.Once);
        
        _customerRepositoryMock.Verify(setup => setup.UnitOfWork.RollbackAsync(),
            Times.Once);
        
        _domainNotificationMock.Verify(setup => setup.PublishInternalServerErrorAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeFalse();
    }

    [Fact(DisplayName = "RegisterCustomerAsync when customer is valid returns registered customer")]
    [Trait("CustomerDomainService", "RegisterCustomerAsync")]
    public async Task RegisterCustomerAsync_WhenCustomerIsValid_ReturnsRegisteredCustomer()
    {
        // Arrange
        var wallet = _walletFaker.GetValid();
        var customer = _customerFaker.GetValid();
        var hashedPassword = _internetFaker.Password();

        var expectedCustomer = customer;
        expectedCustomer.SetPassword(hashedPassword);
        
        _customerRepositoryMock.Setup(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()))
            .ReturnsAsync(false);
        
        _argon2IdHasherMock.Setup(setup => setup.Hash(customer.Password))
            .Returns(hashedPassword);

        _walletDomainServiceMock.Setup(setup => setup.CreateNewEmptyWalletAsync())
            .ReturnsAsync(wallet);

        _customerRepositoryMock.Setup(setup => setup.UnitOfWork.BeginTransactionAsync())
            .Verifiable();
        
        _customerRepositoryMock.Setup(setup => setup.Create(It.IsAny<Customer>()))
            .Verifiable();
        
        _customerRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();
        
        _customerRepositoryMock.Setup(setup => setup.UnitOfWork.CommitAsync())
            .Verifiable();

        // Act
        var result = await _sut.RegisterCustomerAsync(customer);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.ExistsAsync(
                entity => entity.Email.ToLower() == customer.Email.ToLower()),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(It.IsAny<string>()),
            Times.Once);

        _walletDomainServiceMock.Verify(setup => setup.CreateNewEmptyWalletAsync(),
            Times.Once);

        _customerRepositoryMock.Verify(setup => setup.UnitOfWork.BeginTransactionAsync(),
            Times.Once);
        
        _customerRepositoryMock.Verify(setup => setup.Create(It.IsAny<Customer>()),
            Times.Once);
        
        _customerRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        
        _customerRepositoryMock.Verify(setup => setup.UnitOfWork.CommitAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeTrue();

        result.Value.Should()
            .BeEquivalentTo(expectedCustomer);
    }
    
    #endregion

    #region ResetCustomerPasswordAsync

    #endregion
}