using Bogus;
using Bogus.DataSets;
using EscNet.Hashers.Interfaces.Algorithms;
using FluentAssertions;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
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
    
    private readonly Person _personFaker;
    private readonly Internet _internetFaker;
    
    #endregion

    #region Constructor

    public CustomerDomainServiceTests()
    {
        _personFaker = new Person();
        _internetFaker = new Internet();
        
        _customerFaker = new CustomerFaker();

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

        _customerRepositoryMock.Setup(s => s.GetAsync(
                q => q.Id == Guid.Parse(customerId),
                string.Empty,
                true))
            .ReturnsAsync(customer);

        // Act 
        var result = await _sut.GetCustomerAsync(customerId);

        // Assert
        _customerRepositoryMock.Verify(v => v.GetAsync(
                q => q.Id == Guid.Parse(customerId),
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

        _customerRepositoryMock.Setup(s => s.GetAsync(
                q => q.Id == Guid.Parse(customerId),
                string.Empty,
                true))
            .ReturnsAsync(() => null);

        // Act 
        var result = await _sut.GetCustomerAsync(customerId);

        // Assert
        _customerRepositoryMock.Verify(v => v.GetAsync(
                q => q.Id == Guid.Parse(customerId),
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

        _customerRepositoryMock.Setup(s => s.ExistsAsync(
                q => q.Email.ToLower() == email.ToLower()))
            .ReturnsAsync(false);
        
        _domainNotificationMock.Setup(s=>s.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerRepositoryMock.Verify(v => v.ExistsAsync(
                q => q.Email.ToLower() == email.ToLower()),
            Times.Once);
        
        _domainNotificationMock.Verify(v=>v.PublishEmailAndOrPasswordMismatchAsync(),
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

        _customerRepositoryMock.Setup(s => s.ExistsAsync(
                q => q.Email.ToLower() == email.ToLower()))
            .ReturnsAsync(true);
        
        _customerRepositoryMock.Setup(s => s.GetAsync(
                q => q.Email.ToLower() == email.ToLower(),
                "Wallet",
                true))
            .ReturnsAsync(customer);

        _argon2IdHasherMock.Setup(s => s.Hash(password))
            .Returns(hashedPassword);
        
        _domainNotificationMock.Setup(s=>s.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerRepositoryMock.Verify(v => v.ExistsAsync(
                q => q.Email.ToLower() == email.ToLower()),
            Times.Once);

        _customerRepositoryMock.Verify(s => s.GetAsync(
                q => q.Email.ToLower() == email.ToLower(),
                "Wallet",
                true),
            Times.Once);

        _argon2IdHasherMock.Verify(v => v.Hash(password),
            Times.Once);

        _domainNotificationMock.Verify(v=>v.PublishEmailAndOrPasswordMismatchAsync(),
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
        
        _customerRepositoryMock.Setup(s => s.ExistsAsync(
                q => q.Email.ToLower() == email.ToLower()))
            .ReturnsAsync(true);
        
        _customerRepositoryMock.Setup(s => s.GetAsync(
                q => q.Email.ToLower() == email.ToLower(),
                "Wallet",
                true))
            .ReturnsAsync(customer);

        _argon2IdHasherMock.Setup(s => s.Hash(password))
            .Returns(hashedPassword);

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerRepositoryMock.Verify(v => v.ExistsAsync(
                q => q.Email.ToLower() == email.ToLower()),
            Times.Once);

        _customerRepositoryMock.Verify(s => s.GetAsync(
                q => q.Email.ToLower() == email.ToLower(),
                "Wallet",
                true),
            Times.Once);

        _argon2IdHasherMock.Verify(v => v.Hash(password),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.HasValue.Should()
            .BeTrue();

        result.Value.Should()
            .BeEquivalentTo(customer);
    }

    #endregion
}