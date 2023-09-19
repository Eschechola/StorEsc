using Bogus;
using Bogus.DataSets;
using EscNet.Hashers.Interfaces.Algorithms;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices.Services;

public class AdministratorDomainServiceTests
{
    #region Properties

    private readonly IAdministratorDomainService _sut;

    private readonly Mock<IAdministratorRepository> _administratorRepositoryMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationMock;
    private readonly Mock<IArgon2IdHasher> _argon2IdHasherMock;
    private readonly IConfiguration _configuration;

    private readonly AdministratorFaker _administratorFaker;

    private readonly Person _personFaker;
    private readonly Internet _internetFaker;
    
    #endregion

    #region Constructor

    public AdministratorDomainServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Administrator:Email", "test@email.com"}
        };

        _administratorRepositoryMock = new Mock<IAdministratorRepository>();
        _domainNotificationMock = new Mock<IDomainNotificationFacade>();
        _argon2IdHasherMock = new Mock<IArgon2IdHasher>();
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _administratorFaker = new AdministratorFaker();
        
        _personFaker = new Person();
        _internetFaker = new Internet();
        
        _sut = new AdministratorDomainService(
            administratorRepository: _administratorRepositoryMock.Object,
            domainNotification: _domainNotificationMock.Object,
            argon2IdHasher: _argon2IdHasherMock.Object,
            configuration: _configuration);
    }
    
    #endregion

    #region AuthenticateAdministratorAsync

    [Fact(DisplayName = "AuthenticateAdministratorAsync when email not exists throw password mismatch notification and returns empty optional")]
    [Trait("AdministratorDomainService", "AuthenticateAdministratorAsync")]
    public async Task AuthenticateAdministratorAsync_WhenEmailNotExists_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var email = _personFaker.Email;
        var password = _internetFaker.Password();

        _administratorRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(email))
            .ReturnsAsync(false);
        
        _domainNotificationMock.Setup(setup => setup.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateAdministratorAsync(email, password);

        // Assert
        _administratorRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(email),
            Times.Once);
        
        _domainNotificationMock.Verify(setup => setup.PublishEmailAndOrPasswordMismatchAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "AuthenticateAdministratorAsync when email exists but password is wrong throw password mismatch notification and returns empty optional")]
    [Trait("AdministratorDomainService", "AuthenticateAdministratorAsync")]
    public async Task AuthenticateAdministratorAsync_WhenEmailExistsButPasswordIsWrong_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var administrator = _administratorFaker.GetValid();
        
        var email = administrator.Email;
        var password = _internetFaker.Password();
        var hashedPassword =_internetFaker.Password(); 

        _administratorRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(email))
            .ReturnsAsync(true);
        
        _administratorRepositoryMock.Setup(setup => setup.GetByEmailAsync(email))
            .ReturnsAsync(administrator);

        _argon2IdHasherMock.Setup(setup => setup.Hash(password))
            .Returns(hashedPassword);
        
        _domainNotificationMock.Setup(setup => setup.PublishEmailAndOrPasswordMismatchAsync())
            .Verifiable();

        // Act
        var result = await _sut.AuthenticateAdministratorAsync(email, password);

        // Assert
        _administratorRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(email),
            Times.Once);

        _administratorRepositoryMock.Verify(setup => setup.GetByEmailAsync(email),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(password),
            Times.Once);

        _domainNotificationMock.Verify(setup => setup.PublishEmailAndOrPasswordMismatchAsync(),
            Times.Once);
        
        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "AuthenticateAdministratorAsync when email exists and password is correct returns administrator authenticated")]
    [Trait("AdministratorDomainService", "AuthenticateAdministratorAsync")]
    public async Task AuthenticateAdministratorAsync_WhenEmailExistsAndPasswordIsCorrect_ReturnsAdministratorAuthenticated()
    {
        // Arrange
        var administrator = _administratorFaker.GetValid();
        var email = administrator.Email;
        var password = administrator.Password;
        var hashedPassword = administrator.Password;
        
        _administratorRepositoryMock.Setup(setup => setup.ExistsByEmailAsync(email))
            .ReturnsAsync(true);
        
        _administratorRepositoryMock.Setup(setup => setup.GetByEmailAsync(email))
            .ReturnsAsync(administrator);

        _argon2IdHasherMock.Setup(setup => setup.Hash(password))
            .Returns(hashedPassword);

        // Act
        var result = await _sut.AuthenticateAdministratorAsync(email, password);

        // Assert
        _administratorRepositoryMock.Verify(setup => setup.ExistsByEmailAsync(email),
            Times.Once);

        _administratorRepositoryMock.Verify(setup => setup.GetByEmailAsync(email),
            Times.Once);

        _argon2IdHasherMock.Verify(setup => setup.Hash(password),
            Times.Once);

        result.Should()
            .NotBeNull();

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(administrator);
    }

    #endregion
}