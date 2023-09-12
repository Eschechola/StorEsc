using Bogus;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class VoucherFaker: BaseFaker<Voucher>
{
    private readonly Randomizer _randomizer = new();

    public override Voucher GetValid()
        => new Voucher(
            sellerId: Guid.NewGuid(),
            code: _randomizer.String2(3, 80).ToUpper(),
            valueDiscount: _randomizer.Decimal(1, 1_000_000),
            percentageDiscount: _randomizer.Decimal(0, 100),
            isPercentageDiscount: true,
            enabled: true
        );

    public override Voucher GetInvalid()
        => new Voucher(
            sellerId: Guid.Empty,
            code: "",
            valueDiscount: 0,
            percentageDiscount: 0,
            isPercentageDiscount: true,
            enabled: false
        );
}