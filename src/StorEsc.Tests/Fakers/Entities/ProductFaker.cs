using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class ProductFaker : BaseFaker<Product>
{
    private readonly Commerce _commerceFaker = new();
    private readonly Lorem _loremFaker = new();
    private readonly Randomizer _randomizerFaker = new();

    public override Product GetValid()
        => new Product(
            categoryId: Guid.NewGuid(),
            name: _commerceFaker.ProductName(),
            description: _loremFaker.Paragraph(3),
            price: _randomizerFaker.Decimal(0, 10_000),
            stock: _randomizerFaker.Int(10, 1000),
            enabled: true);

    public override Product GetInvalid()
        => new Product(
            categoryId: Guid.NewGuid(),
            name: "",
            description: "",
            price: 0,
            stock: 0,
            enabled: false);
}