using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class SellerFaker : BaseFaker<Seller>
{
    private readonly Person _personFaker;
    private readonly Internet _internetFaker;

    public SellerFaker()
    {
        _personFaker = new Person();
        _internetFaker = new Internet();
    }

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