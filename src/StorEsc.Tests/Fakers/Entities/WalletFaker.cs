using Bogus;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class WalletFaker : BaseFaker<Wallet>
{
    private readonly Randomizer _randomizer = new();

    public override Wallet GetValid()
        => new Wallet(
            id: Guid.NewGuid(),
            amount: _randomizer.Decimal(100, 10000));

    public override Wallet GetInvalid()
        => new Wallet(
            id: Guid.Empty,
            amount: 0);
}