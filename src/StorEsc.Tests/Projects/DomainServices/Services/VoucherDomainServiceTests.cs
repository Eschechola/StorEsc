using FluentAssertions;
using Moq;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Tests.Fakers.Entities;
using Xunit;

namespace StorEsc.Tests.Projects.DomainServices.Services;

public class VoucherDomainServiceTests
{
    #region Properties

    private readonly IVoucherDomainService _sut;

    private readonly Mock<IVoucherRepository> _voucherRepositoryMock;
    private readonly Mock<IDomainNotificationFacade> _domainNotificationFacade;

    private readonly VoucherFaker _voucherFaker;
    
    #endregion

    #region Constructor

    public VoucherDomainServiceTests()
    {
        _voucherRepositoryMock = new Mock<IVoucherRepository>();
        _domainNotificationFacade = new Mock<IDomainNotificationFacade>();

        _voucherFaker = new VoucherFaker();
        
        _sut = new VoucherDomainService(
            voucherRepository: _voucherRepositoryMock.Object,
            domainNotificationFacade: _domainNotificationFacade.Object);
    }

    #endregion

    #region GetAllVouchersAsync
    
    [Fact(DisplayName = "VoucherDomainService when all is valid return all vouchers")]
    [Trait("VoucherDomainService", "GetAllVouchersAsync")]
    private async Task VoucherDomainService_WhenAdministratorIsValid_ReturnAllVouchers()
    {
        // Arrange
        var vouchers = _voucherFaker.GetValidList();
        
        _voucherRepositoryMock.Setup(setup => setup.GetAllAsync(
                null,
                "",
                null,
                true,
                null))
            .ReturnsAsync(vouchers);
        
        // Act
        var result = await _sut.GetAllVouchersAsync();

        // Assert
        _voucherRepositoryMock.Verify(verify => verify.GetAllAsync(
                null,
                "",
                null,
                true,
                null),
            Times.Once);
        
        result.Should()
            .BeEquivalentTo(vouchers);
    }

    #endregion

    #region CreateVoucherAsync
    
    [Fact(DisplayName = "CreateVoucherAsync when voucher is not valid throw notification and returns empty optional")]
    [Trait("VoucherDomainService", "CreateVoucherAsync")]
    private async Task CreateVoucherAsync_WhenVoucherIsNotValid_ThrowNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var voucher = _voucherFaker.GetInvalid();

        _domainNotificationFacade.Setup(setup => setup.PublishEntityDataIsInvalidAsync(It.IsAny<string>()))
            .Verifiable();
        
        // Act
        var result = await _sut.CreateVoucherAsync(voucher);

        // Assert
        _domainNotificationFacade.Verify(verify => verify.PublishEntityDataIsInvalidAsync(It.IsAny<string>()),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "CreateVoucherAsync when voucher already exists throw notification and returns empty optional")]
    [Trait("VoucherDomainService", "CreateVoucherAsync")]
    private async Task CreateVoucherAsync_WhenVoucherAlreadyExists_ThrowNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var voucher = _voucherFaker.GetValid();
        
        _domainNotificationFacade.Setup(setup => setup.PublishAlreadyExistsAsync("Voucher"))
            .Verifiable();

        _voucherRepositoryMock.Setup(setup => setup.ExistsAsync(
                query => query.Code.ToLower().Equals(voucher.Code.ToLower())))
            .ReturnsAsync(true);
        
        // Act
        var result = await _sut.CreateVoucherAsync(voucher);

        // Assert
        _voucherRepositoryMock.Verify(verify => verify.ExistsAsync(
                query => query.Code.ToLower().Equals(voucher.Code.ToLower())),
            Times.Once);
        
        _domainNotificationFacade.Verify(verify => verify.PublishAlreadyExistsAsync("Voucher"),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "CreateVoucherAsync when voucher is valid create and returns created voucher")]
    [Trait("VoucherDomainService", "CreateVoucherAsync")]
    private async Task CreateVoucherAsync_WhenVoucherIsValid_CreateAndReturnsCreatedVoucher()
    {
        // Arrange
        var voucher = _voucherFaker.GetValid();
        var voucherCreated = voucher;
        
        voucherCreated.CodeToUpper();
        voucherCreated.SetDiscounts();
        voucherCreated.Disable();

        _domainNotificationFacade.Setup(setup => setup.PublishAlreadyExistsAsync("Voucher"))
            .Verifiable();

        _voucherRepositoryMock.Setup(setup => setup.ExistsAsync(
                query => query.Code.ToLower().Equals(voucher.Code.ToLower())))
            .ReturnsAsync(false);
        
        _voucherRepositoryMock.Setup(setup => setup.Create(voucherCreated))
            .Verifiable();
        
        _voucherRepositoryMock.Setup(setup => setup.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();
        
        // Act
        var result = await _sut.CreateVoucherAsync(voucher);

        // Assert
        _voucherRepositoryMock.Verify(verify => verify.ExistsAsync(
                query => query.Code.ToLower().Equals(voucher.Code.ToLower())),
            Times.Once);
        
        _voucherRepositoryMock.Verify(verify => verify.Create(voucherCreated),
            Times.Once);
        
        _voucherRepositoryMock.Verify(verify => verify.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        
        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(voucherCreated);
    }

    #endregion
}