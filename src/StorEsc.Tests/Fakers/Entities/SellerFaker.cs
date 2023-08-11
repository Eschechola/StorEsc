using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class SellerFaker : BaseFaker<Seller>
{
    private readonly Person _personFaker = new();
    private readonly Internet _internetFaker = new();

    public override Seller GetValid()
        => new Seller(
            walletId: Guid.NewGuid(),
            firstName: _personFaker.FirstName,
            lastName: _personFaker.LastName,
            email: _personFaker.Email,
            password: _internetFaker.Password());

    public override Seller GetInvalid()
        => new Seller(
            walletId: Guid.Empty,
            firstName: "",
            lastName: "",
            email: "",
            password: "");
}