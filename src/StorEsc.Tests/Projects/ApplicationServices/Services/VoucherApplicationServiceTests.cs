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
        string administratorId = Guid.NewGuid().ToString();

        _voucherDomainServiceMock.Setup(setup => setup.GetAllVouchersAsync(administratorId))
            .ReturnsAsync(new List<Voucher>());

        // Act
        var result = await _sut.GetAllVouchersAsync(administratorId);

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.GetAllVouchersAsync(administratorId),
            Times.Once);
        
        result.Should()
            .BeEmpty();
    }
    
    [Fact(DisplayName = "GetAllVouchersAsync when vouchers has been found returns voucher list")]
    [Trait("VoucherApplicationService", "GetAllVouchersAsync")]
    public async Task GetAllVouchersAsync_WhenVouchersHasBeenFoundReturnsVoucherList()
    {
        // Arrange
        string administratorId = Guid.NewGuid().ToString();
        var vouchers = _voucherFaker.GetValidList();

        _voucherDomainServiceMock.Setup(setup => setup.GetAllVouchersAsync(administratorId))
            .ReturnsAsync(vouchers);

        // Act
        var result = await _sut.GetAllVouchersAsync(administratorId);

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.GetAllVouchersAsync(administratorId),
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
        var administratorId = Guid.NewGuid().ToString();
        var voucherDto = _voucherFaker.GetValid().AsDto();

        _voucherDomainServiceMock.Setup(setup => setup.CreateVoucherAsync(administratorId, It.IsAny<Voucher>()))
            .ReturnsAsync(new Optional<Voucher>());

        // Act
        var result = await _sut.CreateVoucherAsync(administratorId, voucherDto);

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.CreateVoucherAsync(administratorId, It.IsAny<Voucher>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeTrue();
    }
    
    [Fact(DisplayName = "CreateVoucherAsync when voucher has been created returns created voucher")]
    [Trait("VoucherApplicationService", "CreateVoucherAsync")]
    public async Task CreateVoucherAsync_WhenVoucherHasBeenCreated_ReturnsCreatedVoucher()
    {
        // Arrange
        var administratorId = Guid.NewGuid().ToString();
        var voucher = _voucherFaker.GetValid();

        _voucherDomainServiceMock.Setup(setup => setup.CreateVoucherAsync(administratorId, It.IsAny<Voucher>()))
            .ReturnsAsync(voucher);

        // Act
        var result = await _sut.CreateVoucherAsync(administratorId, voucher.AsDto());

        // Assert
        _voucherDomainServiceMock.Verify(verify => verify.CreateVoucherAsync(administratorId, It.IsAny<Voucher>()),
            Times.Once);

        result.IsEmpty.Should()
            .BeFalse();

        result.Value.Should()
            .BeEquivalentTo(voucher.AsDto());
    }

    #endregion
}