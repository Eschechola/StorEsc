using Bogus;
using FluentAssertions;
using Moq;
using StorEsc.Application.Extensions;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.ApplicationServices.Services;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.ApplicationServices.Services;

public class RechargeApplicationServiceTests
{
    #region Properties

    private readonly IRechargeApplicationService _sut;

    private readonly Mock<IRechargeDomainService> _rechargeDomainServiceMock;

    private readonly CreditCardFaker _creditCardFaker;
    private readonly Randomizer _randomizer;
    
    #endregion

    #region Constructor

    public RechargeApplicationServiceTests()
    {
        _creditCardFaker = new CreditCardFaker();
        _randomizer = new Randomizer();

        _rechargeDomainServiceMock = new Mock<IRechargeDomainService>();

        _sut = new RechargeApplicationService(
            rechargeDomainService: _rechargeDomainServiceMock.Object);
    }

    #endregion

    #region RechargeCustomerWalletAsync

    [Fact(DisplayName = "RechargeCustomerWalletAsync when recharge has been done returns true")]
    [Trait("RechargeApplicationService", "RechargeCustomerWalletAsync")]
    public async Task RechargeCustomerWalletAsync_WhenRechargeHasBeenDone_ReturnsTrue()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();
        var amount = _randomizer.Decimal(0, 10_000);
        var creditCardDto = _creditCardFaker.GetValid().AsDto();

        _rechargeDomainServiceMock
            .Setup(setup => setup.RechargeCustomerWalletAsync(customerId, amount, It.IsAny<CreditCard>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.RechargeCustomerWalletAsync(customerId, amount, creditCardDto);

        // Assert
        _rechargeDomainServiceMock.Verify(verify => verify.RechargeCustomerWalletAsync(customerId, amount, It.IsAny<CreditCard>()),
                Times.Once);
        
        result.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "RechargeCustomerWalletAsync when recharge not done returns false")]
    [Trait("RechargeApplicationService", "RechargeCustomerWalletAsync")]
    public async Task RechargeCustomerWalletAsync_WhenRechargeNotDone_ReturnsFalse()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();
        var amount = _randomizer.Decimal(0, 10_000);
        var creditCardDto = _creditCardFaker.GetValid().AsDto();

        _rechargeDomainServiceMock
            .Setup(setup => setup.RechargeCustomerWalletAsync(customerId, amount, It.IsAny<CreditCard>()))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.RechargeCustomerWalletAsync(customerId, amount, creditCardDto);

        // Assert
        _rechargeDomainServiceMock.Verify(verify => verify.RechargeCustomerWalletAsync(customerId, amount, It.IsAny<CreditCard>()),
            Times.Once);
        
        result.Should()
            .BeFalse();
    }
    
    #endregion
}