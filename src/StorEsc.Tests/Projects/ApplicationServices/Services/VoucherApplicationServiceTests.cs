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

public class VoucherApplicationServiceTests
{
    #region Properties

    private readonly IVoucherApplicationService _sut;

    private readonly Mock<IVoucherDomainService> _voucherDomainServiceMock;

    private readonly VoucherFaker _voucherFaker;

    #endregion

    #region Constructor

    public VoucherApplicationServiceTests()
    {
        _voucherFaker = new VoucherFaker();

        _voucherDomainServiceMock = new Mock<IVoucherDomainService>();

        _sut = new VoucherApplicationService(
            voucherDomainService: _voucherDomainServiceMock.Object);
    }

    #endregion

    #region GetAllVouchersAsync

    [Fact(DisplayName = "GetAllVouchersAsync when vouchers not found returns empty list")]
    [Trait("VoucherApplicationService", "GetAllVouchersAsync")]
    public async Task GetAllVouchersAsync_WhenVouchersNotFoundReturnsEmptyList()
    {
        // Arrange
        _voucherDomainServiceMock.Setup(setup => setup.GetAllVouchersAsync())
            .ReturnsAsync(new List<Voucher>());

        // Act
        var result = await _sut.GetAllVouchersAsync();

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.GetAllVouchersAsync(),
            Times.Once);
        
        result.Should()
            .BeEmpty();
    }
    
    [Fact(DisplayName = "GetAllVouchersAsync when vouchers has been found returns voucher list")]
    [Trait("VoucherApplicationService", "GetAllVouchersAsync")]
    public async Task GetAllVouchersAsync_WhenVouchersHasBeenFoundReturnsVoucherList()
    {
        // Arrange
        var vouchers = _voucherFaker.GetValidList();

        _voucherDomainServiceMock.Setup(setup => setup.GetAllVouchersAsync())
            .ReturnsAsync(vouchers);

        // Act
        var result = await _sut.GetAllVouchersAsync();

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.GetAllVouchersAsync(),
            Times.Once);
        
        result.Should()
            .BeEquivalentTo(vouchers, options =>
                options.Excluding(entity => entity.Errors)
                    .Excluding(entity => entity.Orders));
    }

    #endregion

    #region CreateVoucherAsync

    [Fact(DisplayName = "CreateVoucherAsync when voucher not created returns empty optional")]
    [Trait("VoucherApplicationService", "CreateVoucherAsync")]
    public async Task CreateVoucherAsync_WhenVoucherNotCreated_ReturnsEmptyOptional()
    {
        // Arrange
        var voucherDto = _voucherFaker.GetValid().AsDto();

        _voucherDomainServiceMock.Setup(setup => setup.CreateVoucherAsync(It.IsAny<Voucher>()))
            .ReturnsAsync(new Optional<Voucher>());

        // Act
        var result = await _sut.CreateVoucherAsync(voucherDto);

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.CreateVoucherAsync(It.IsAny<Voucher>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "CreateVoucherAsync when voucher has been created returns created voucher")]
    [Trait("VoucherApplicationService", "CreateVoucherAsync")]
    public async Task CreateVoucherAsync_WhenVoucherHasBeenCreated_ReturnsCreatedVoucher()
    {
        // Arrange
        var voucher = _voucherFaker.GetValid();

        _voucherDomainServiceMock.Setup(setup => setup.CreateVoucherAsync(It.IsAny<Voucher>()))
            .ReturnsAsync(voucher);

        // Act
        var result = await _sut.CreateVoucherAsync(voucher.AsDto());

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.CreateVoucherAsync(It.IsAny<Voucher>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(voucher.AsDto());
    }

    #endregion

    #region EnableVoucherAsync

    [Fact(DisplayName = "EnableVoucherAsync when voucher has been enabled returns true")]
    [Trait("VoucherApplicationService", "EnableVoucherAsync")]
    public async Task EnableVoucherAsync_WhenVoucherHasBeenEnabled_ReturnsTrue()
    {
        // Arrange
        var voucherId = Guid.NewGuid().ToString();

        _voucherDomainServiceMock.Setup(setup => setup.EnableVoucherAsync(voucherId))
            .ReturnsAsync(true);
        
        // Act
        var result = await _sut.EnableVoucherAsync(voucherId);

        // Assert
        _voucherDomainServiceMock.Verify(setup => setup.EnableVoucherAsync(voucherId),
            Times.Once);
        
        result.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "EnableVoucherAsync when voucher not enabled returns false")]
    [Trait("VoucherApplicationService", "EnableVoucherAsync")]
    public async Task EnableVoucherAsync_WhenVoucherNotEnabled_ReturnsFalse()
    {
        // Arrange
        var voucherId = Guid.NewGuid().ToString();

        _voucherDomainServiceMock.Setup(setup => setup.EnableVoucherAsync(voucherId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _sut.EnableVoucherAsync(voucherId);

        // Assert
        _voucherDomainServiceMock.Verify(setup => setup.EnableVoucherAsync(voucherId),
            Times.Once);
        
        result.Should()
            .BeFalse();
    }
    
    #endregion
    
    #region DisableVoucherAsync

    [Fact(DisplayName = "DisableVoucherAsync when voucher has been disabled returns true")]
    [Trait("VoucherApplicationService", "DisableVoucherAsync")]
    public async Task DisableVoucherAsync_WhenVoucherHasBeenDisabled_ReturnsTrue()
    {
        // Arrange
        var voucherId = Guid.NewGuid().ToString();

        _voucherDomainServiceMock.Setup(setup => setup.DisableVoucherAsync(voucherId))
            .ReturnsAsync(true);
        
        // Act
        var result = await _sut.DisableVoucherAsync(voucherId);

        // Assert
        _voucherDomainServiceMock.Verify(setup => setup.DisableVoucherAsync(voucherId),
            Times.Once);
        
        result.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "DisableVoucherAsync when voucher not disabled returns false")]
    [Trait("VoucherApplicationService", "DisableVoucherAsync")]
    public async Task DisableVoucherAsync_WhenVoucherNotDisabled_ReturnsFalse()
    {
        // Arrange
        var voucherId = Guid.NewGuid().ToString();

        _voucherDomainServiceMock.Setup(setup => setup.DisableVoucherAsync(voucherId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _sut.DisableVoucherAsync(voucherId);

        // Assert
        _voucherDomainServiceMock.Verify(setup => setup.DisableVoucherAsync(voucherId),
            Times.Once);
        
        result.Should()
            .BeFalse();
    }
    
    #endregion

    #region UpdateVoucherAsync

    [Fact(DisplayName = "UpdateVoucherAsync when voucher not updated returns empty optional")]
    [Trait("VoucherApplicationService", "UpdateVoucherAsync")]
    public async Task UpdateVoucherAsync_WhenVoucherNotUpdated_ReturnsEmptyOptional()
    {
        // Arrange
        var voucherDto = _voucherFaker.GetValid().AsDto();
        var voucherId = voucherDto.Id.ToString();

        _voucherDomainServiceMock.Setup(setup => setup.UpdateVoucherAsync(voucherId, It.IsAny<Voucher>()))
            .ReturnsAsync(new Optional<Voucher>());

        // Act
        var result = await _sut.UpdateVoucherAsync(voucherId, voucherDto);

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.UpdateVoucherAsync(voucherId, It.IsAny<Voucher>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    
    [Fact(DisplayName = "UpdateVoucherAsync when voucher has been updated returns voucher")]
    [Trait("VoucherApplicationService", "UpdateVoucherAsync")]
    public async Task UpdateVoucherAsync_WhenVoucherHasBeenUpdated_ReturnsVoucher()
    {
        // Arrange
        var voucherDto = _voucherFaker.GetValid().AsDto();
        var voucherId = voucherDto.Id.ToString();

        _voucherDomainServiceMock.Setup(setup => setup.UpdateVoucherAsync(voucherId, It.IsAny<Voucher>()))
            .ReturnsAsync(new Optional<Voucher>(voucherDto.AsEntity()));

        // Act
        var result = await _sut.UpdateVoucherAsync(voucherId, voucherDto);

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.UpdateVoucherAsync(voucherId, It.IsAny<Voucher>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(voucherDto);
    }
    
    
    #endregion
}