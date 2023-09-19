using Bogus;
using FluentAssertions;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices.Services;

public class RechargeDomainServiceTests
{
    #region Properties

    private readonly IRechargeDomainService _sut;

    private readonly Mock<IRechargeRepository> _rechargeRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IPaymentDomainService> _paymentDomainServiceMock;
    private readonly Mock<IWalletDomainService> _walletDomainServiceMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationFacadeMock;

    private readonly CreditCardFaker _creditCardFaker;
    private readonly PaymentFaker _paymentFaker;
    private readonly CustomerFaker _customerFaker;

    private readonly Randomizer _randomizerFaker;
    
    #endregion

    #region Constructor

    public RechargeDomainServiceTests()
    {
        _rechargeRepositoryMock = new Mock<IRechargeRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _paymentDomainServiceMock = new Mock<IPaymentDomainService>();
        _walletDomainServiceMock = new Mock<IWalletDomainService>();
        _domainNotificationFacadeMock = new Mock<IDomainNotificationFacade>();

        _creditCardFaker = new CreditCardFaker();
        _paymentFaker = new PaymentFaker();
        _customerFaker = new CustomerFaker();

        _randomizerFaker = new Randomizer();
        
        _sut = new RechargeDomainService(
            rechargeRepository: _rechargeRepositoryMock.Object,
            customerRepository: _customerRepositoryMock.Object,
            paymentDomainService: _paymentDomainServiceMock.Object,
            walletDomainService: _walletDomainServiceMock.Object,
            domainNotificationFacade: _domainNotificationFacadeMock.Object
        );
    }

    #endregion

    #region RechargeCustomerWalletAsync

    [Fact(DisplayName = "RechargeCustomerWalletAsync when payment is refused throw notification and returns false")]
    [Trait("RechargeDomainService", "RechargeCustomerWalletAsync")]
    public async Task RechargeCustomerWalletAsync_WhenPaymentIsRefused_ThrowNotificationAndReturnsFalse()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();
        var amount = _randomizerFaker.Decimal(0, 1000);
        var creditCard = _creditCardFaker.GetValid();
        var payment = _paymentFaker.GetValid(isPaid: false);


        _paymentDomainServiceMock.Setup(setup => setup.PayRechargeAsync(
                amount,
                creditCard))
            .ReturnsAsync(payment);
        
        _domainNotificationFacadeMock.Setup(setup => setup.PublishPaymentRefusedAsync())
            .Verifiable();

        // Act
        var result = await _sut.RechargeCustomerWalletAsync(customerId, amount, creditCard);

        // Assert
        _domainNotificationFacadeMock.Verify(setup => setup.PublishPaymentRefusedAsync(),
            Times.Once);
        
        result.Should()
            .BeFalse();
    }
    
    [Fact(DisplayName = "RechargeCustomerWalletAsync when payment is approved create recharge and returns true")]
    [Trait("RechargeDomainService", "RechargeCustomerWalletAsync")]
    public async Task RechargeCustomerWalletAsync_WhenPaymentIsApproved_CreateRechargeAndReturnsTrue()
    {
        // Arrange
        var customerId = Guid.NewGuid().ToString();
        var amount = _randomizerFaker.Decimal(0, 1000);
        var creditCard = _creditCardFaker.GetValid();
        
        var payment = _paymentFaker.GetValid();
        var customer = _customerFaker.GetValid();
        
        _paymentDomainServiceMock.Setup(setup => setup.PayRechargeAsync(
                amount,
                creditCard))
            .ReturnsAsync(payment);

        _customerRepositoryMock.Setup(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true))
            .ReturnsAsync(customer);
        
        _rechargeRepositoryMock.Setup(setup => setup.Create(It.IsAny<Recharge>()))
            .Verifiable();
        
        _rechargeRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();
        
        _walletDomainServiceMock.Setup(setup => setup.AddAmountToWalletAsync(customer.WalletId, amount))
            .Verifiable();

        // Act
        var result = await _sut.RechargeCustomerWalletAsync(customerId, amount, creditCard);

        // Assert
        _customerRepositoryMock.Verify(setup => setup.GetAsync(
                entity => entity.Id == Guid.Parse(customerId),
                string.Empty,
                true),
            Times.Once);

        _rechargeRepositoryMock.Verify(setup => setup.Create(It.IsAny<Recharge>()),
            Times.Once);

        _rechargeRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once());

        _walletDomainServiceMock.Verify(setup => setup.AddAmountToWalletAsync(customer.WalletId, amount),
            Times.Once);

        result.Should()
            .BeTrue();
    }

    #endregion
}