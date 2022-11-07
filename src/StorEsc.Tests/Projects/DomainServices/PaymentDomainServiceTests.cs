using Bogus;
using FluentAssertions;
using Moq;
using StorEsc.Domain.Entities;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.ExternalServices.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using StorEsc.Tests.Fakers.Infrastructure.ExternalServices;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices;

public class PaymentDomainServiceTests
{
    #region Properties

    private readonly IPaymentDomainService _sut;

    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentExternalService> _paymentExternalServiceMock;

    private readonly PaymentRechargeResponseFaker _paymentRechargeResponseFaker;
    private readonly CreditCardFaker _creditCardFaker;

    private readonly Randomizer _randomizer;
    
    #endregion

    #region Constructor

    public PaymentDomainServiceTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _paymentExternalServiceMock = new Mock<IPaymentExternalService>();

        _paymentRechargeResponseFaker = new PaymentRechargeResponseFaker();
        _creditCardFaker = new CreditCardFaker();

        _randomizer = new Randomizer();
        
        _sut = new PaymentDomainService(
            paymentRepository: _paymentRepositoryMock.Object,
            paymentExternalService: _paymentExternalServiceMock.Object);
    }

    #endregion

    #region PayRechargeAsync

    [Fact(DisplayName = "PayRechargeAsync when payment is refused create and return refused payment")]
    [Trait("PaymentDomainService", "PayRechargeAsync")]
    public async Task PaymentDomainService_WhenPaymentIsRefused_CreateAndReturnRefusedPayment()
    {
        // Arrange
        var paymentResponse = _paymentRechargeResponseFaker.GetValid();
        paymentResponse.IsPaid = false;

        var amount = _randomizer.Decimal();
        var creditCard = _creditCardFaker.GetValid();

        var expectedPayment = new Payment(isPaid: false);

        _paymentExternalServiceMock.Setup(setup => setup.PayRechargeAsync(amount, creditCard))
            .ReturnsAsync(paymentResponse);

        _paymentRepositoryMock.Setup(setup => setup.Create(It.IsAny<Payment>()))
            .Verifiable();
        
        _paymentRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(
            It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await _sut.PayRechargeAsync(amount, creditCard);

        // Assert
        _paymentExternalServiceMock.Verify(setup => setup.PayRechargeAsync(amount, creditCard),
            Times.Once);

        _paymentRepositoryMock.Verify(setup => setup.Create(It.IsAny<Payment>()),
            Times.Once);

        _paymentRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        result.Should()
            .NotBeNull();
            
        result.Should()
            .BeEquivalentTo(expectedPayment);
    }

    [Fact(DisplayName = "PayRechargeAsync when payment is accepted create and return accepted payment")]
    [Trait("PaymentDomainService", "PayRechargeAsync")]
    public async Task PaymentDomainService_WhenPaymentIsAccepted_CreateAndReturnAcceptedPayment()
    {
        // Arrange
        var paymentResponse = _paymentRechargeResponseFaker.GetValid();
        paymentResponse.IsPaid = true;

        var amount = _randomizer.Decimal();
        var creditCard = _creditCardFaker.GetValid();

        var expectedPayment = new Payment(isPaid: true, hash: paymentResponse.Hash);

        _paymentExternalServiceMock.Setup(setup => setup.PayRechargeAsync(amount, creditCard))
            .ReturnsAsync(paymentResponse);

        _paymentRepositoryMock.Setup(setup => setup.Create(It.IsAny<Payment>()))
            .Verifiable();
        
        _paymentRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(
                It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await _sut.PayRechargeAsync(amount, creditCard);

        // Assert
        _paymentExternalServiceMock.Verify(setup => setup.PayRechargeAsync(amount, creditCard),
            Times.Once);

        _paymentRepositoryMock.Verify(setup => setup.Create(It.IsAny<Payment>()),
            Times.Once);

        _paymentRepositoryMock.Verify(setup => setup.UnitOfWork.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        result.Should()
            .NotBeNull();
            
        result.Should()
            .BeEquivalentTo(expectedPayment);
    }

    #endregion
}