using Bogus;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class WalletFaker : IFaker<Wallet>
{
    private readonly Randomizer _randomizer;

    public WalletFaker()
    {
        _randomizer = new Randomizer();
    }

    public Wallet GetValid()
        => new Wallet(
            id: Guid.NewGuid(),
            amount: _randomizer.Decimal(100, 10000));

    public Wallet GetInvalid()
        => new Wallet(
            id: Guid.Empty,
            amount: 0);
}