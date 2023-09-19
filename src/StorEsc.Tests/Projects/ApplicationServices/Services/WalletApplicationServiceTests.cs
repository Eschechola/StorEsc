using FluentAssertions;
using Moq;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.ApplicationServices.Services;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.ApplicationServices.Services;

public class WalletApplicationServiceTests
{
    #region Properties

    private readonly IWalletApplicationService _sut;

    private readonly Mock<IWalletDomainService> _walletDomainServiceMock;

    private readonly WalletFaker _walletFaker;

    #endregion

    #region Constructor

    public WalletApplicationServiceTests()
    {
        _walletFaker = new WalletFaker();

        _walletDomainServiceMock = new Mock<IWalletDomainService>();

        _sut = new WalletApplicationService(
            walletDomainService: _walletDomainServiceMock.Object);
    }

    #endregion

    #region GetCustomerWalletAsync

    [Fact(DisplayName = "GetCustomerWalletAsync returns customer wallet")]
    [Trait("WalletApplicationService", "GetCustomerWalletAsync")]
    public async Task GetCustomerWalletAsync_ReturnsCustomerWallet()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();
        var wallet = _walletFaker.GetValid();

        _walletDomainServiceMock.Setup(setup => setup.GetCustomerWalletAsync(customerId))
            .ReturnsAsync(wallet);

        // Act
        var result = await _sut.GetCustomerWalletAsync(customerId);

        // Assert
        _walletDomainServiceMock.Verify(verify => verify.GetCustomerWalletAsync(customerId),
            Times.Once);
        
        result.Should()
            .BeEquivalentTo(wallet.AsDto());
    }
    
    
    #endregion
}