using Bogus.DataSets;
using FluentAssertions;
using Moq;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.ApplicationServices.Services;
using StorEsc.Core.Data.Structs;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.ApplicationServices.Services;

public class AuthApplicationServiceTests
{
    #region Properties

    private readonly IAuthApplicationService _sut;

    private readonly Mock<ICustomerDomainService> _customerDomainServiceMock;
    private readonly Mock<IAdministratorDomainService> _administratorDomainServiceMock;

    private readonly Internet _internetFaker;
    private readonly AdministratorFaker _administratorFaker;
    private readonly CustomerFaker _customerFaker;
    
    #endregion


    #region Constructor

    public AuthApplicationServiceTests()
    {
        _internetFaker = new Internet();
        _administratorFaker = new AdministratorFaker();
        _customerFaker = new CustomerFaker();

        _customerDomainServiceMock = new Mock<ICustomerDomainService>();
        _administratorDomainServiceMock = new Mock<IAdministratorDomainService>();

        _sut = new AuthApplicationService(
            customerDomainService: _customerDomainServiceMock.Object,
            administratorDomainService: _administratorDomainServiceMock.Object);
    }

    #endregion

    #region AuthenticateCustomerAsync

    [Fact(DisplayName = "AuthenticateCustomerAsync when customer not authenticated returns empty optional")]
    [Trait("AuthApplicationService", "AuthenticateCustomerAsync")]
    public async Task AuthenticateCustomerAsync_WhenCustomerNotAuthenticated_ReturnsEmptyOptional()
    {
        // Arrange
        var email = _internetFaker.Email();
        var password = _internetFaker.Password();

        _customerDomainServiceMock.Setup(setup => setup.AuthenticateCustomerAsync(email, password))
            .ReturnsAsync(new Optional<Customer>());

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerDomainServiceMock.Verify(verify => verify.AuthenticateCustomerAsync(email, password),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "AuthenticateCustomerAsync when customer has been authenticated returns authenticated customer")]
    [Trait("AuthApplicationService", "AuthenticateCustomerAsync")]
    public async Task AuthenticateCustomerAsync_WhenCustomerHasBeenAuthenticated_ReturnsAuthenticatedCustomer()
    {
        // Arrange
        var email = _internetFaker.Email();
        var password = _internetFaker.Password();
        var customer = _customerFaker.GetValid();

        _customerDomainServiceMock.Setup(setup => setup.AuthenticateCustomerAsync(email, password))
            .ReturnsAsync(customer);

        // Act
        var result = await _sut.AuthenticateCustomerAsync(email, password);

        // Assert
        _customerDomainServiceMock.Verify(verify => verify.AuthenticateCustomerAsync(email, password),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(customer, options => 
                options.Excluding(exclude => exclude.Errors));
    }

    #endregion

    #region RegisterCustomerAsync

    [Fact(DisplayName = "RegisterCustomerAsync when customer not registered returns empty optional")]
    [Trait("AuthApplicationService", "RegisterCustomerAsync")]
    public async Task RegisterCustomerAsync_WhenCustomerNotRegistered_ReturnsEmptyOptional()
    {
        // Arrange
        var customer = _customerFaker.GetValid();

        _customerDomainServiceMock.Setup(setup => setup.RegisterCustomerAsync(customer))
            .ReturnsAsync(new Optional<Customer>());

        // Act
        var result = await _sut.RegisterCustomerAsync(customer.AsDto());

        // Assert
        _customerDomainServiceMock.Verify(verify => verify.RegisterCustomerAsync(It.IsAny<Customer>()),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "RegisterCustomerAsync when customer has been registered returns registered customer")]
    [Trait("AuthApplicationService", "RegisterCustomerAsync")]
    public async Task RegisterCustomerAsync_WhenCustomerHasBeenRegistered_ReturnsRegisteredCustomer()
    {
        // Arrange
        var customer = _customerFaker.GetValid();

        _customerDomainServiceMock.Setup(setup => setup.RegisterCustomerAsync(It.IsAny<Customer>()))
            .ReturnsAsync(customer);

        // Act
        var result = await _sut.RegisterCustomerAsync(customer.AsDto());

        // Assert
        _customerDomainServiceMock.Verify(verify => verify.RegisterCustomerAsync(It.IsAny<Customer>()),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(customer, options => 
                options.Excluding(exclude => exclude.Errors));
    }

    #endregion


    #region AuthenticateAdministratorAsync

    [Fact(DisplayName = "AuthenticateAdministratorAsync when administrator not authenticated returns empty optional")]
    [Trait("AuthApplicationService", "AuthenticateAdministratorAsync")]
    public async Task AuthenticateAdministratorAsync_WhenAdministratorNotAuthenticated_ReturnsEmptyOptional()
    {
        // Arrange
        var email = _internetFaker.Email();
        var password = _internetFaker.Password();

        _administratorDomainServiceMock.Setup(setup => setup.AuthenticateAdministratorAsync(email, password))
            .ReturnsAsync(new Optional<Administrator>());

        // Act
        var result = await _sut.AuthenticateAdministratorAsync(email, password);

        // Assert
        _administratorDomainServiceMock.Verify(verify => verify.AuthenticateAdministratorAsync(email, password),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "AuthenticateAdministratorAsync when administrator has been authenticated returns authenticated administrator")]
    [Trait("AuthApplicationService", "AuthenticateAdministratorAsync")]
    public async Task AuthenticateAdministratorAsync_WhenAdministratorHasBeenAuthenticated_ReturnsAuthenticatedAdministrator()
    {
        // Arrange
        var email = _internetFaker.Email();
        var password = _internetFaker.Password();
        var administrator = _administratorFaker.GetValid();

        _administratorDomainServiceMock.Setup(setup => setup.AuthenticateAdministratorAsync(email, password))
            .ReturnsAsync(administrator);

        // Act
        var result = await _sut.AuthenticateAdministratorAsync(email, password);

        // Assert
        _administratorDomainServiceMock.Verify(verify => verify.AuthenticateAdministratorAsync(email, password),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(administrator, options => 
                options.Excluding(exclude => exclude.Errors));
    }

    #endregion
}