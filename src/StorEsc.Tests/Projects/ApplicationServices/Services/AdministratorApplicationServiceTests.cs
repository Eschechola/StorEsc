using FluentAssertions;
using Moq;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.ApplicationServices.Services;
using StorEsc.DomainServices.Interfaces;
using Xunit;

namespace StorEsc.Tests.Projects.ApplicationServices.Services;

public class AdministratorApplicationServiceTests
{
    #region Properties

    private readonly IAdministratorApplicationService _sut;

    private readonly Mock<IAdministratorDomainService> _administratorDomainServiceMock;

    #endregion

    #region Constructor

    public AdministratorApplicationServiceTests()
    {
        _administratorDomainServiceMock = new Mock<IAdministratorDomainService>();

        _sut = new AdministratorApplicationService(
            administratorDomainService: _administratorDomainServiceMock.Object);
    }

    #endregion

    #region EnableDefaultAdministratorAsync

    [Fact(DisplayName = "EnableDefaultAdministratorAsync when administrator has enabled returns true")]
    [Trait("AdministratorApplicationService", "EnableDefaultAdministratorAsync")]
    public async Task EnableDefaultAdministratorAsync_WhenAdministratorHasEnabled_ReturnsTrue()
    {
        // Arrange
        _administratorDomainServiceMock.Setup(setup => setup.EnableDefaultAdministratorAsync())
            .ReturnsAsync(true);

        // Act
        var result = await _sut.EnableDefaultAdministratorAsync();

        // Assert
        result.Should()
            .BeTrue();
    }

    #endregion
}