using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class CustomerFaker : BaseFaker<Customer>
{
    private readonly Person _personFaker = new();
    private readonly Internet _internetFaker = new();

    public override Customer GetValid()
        => new Customer(
            walletId: Guid.NewGuid(),
            firstName: _personFaker.FirstName,
            lastName: _personFaker.LastName,
            email: _personFaker.Email,
            password: _internetFaker.Password());

    public override Customer GetInvalid()
        => new Customer(
            walletId: Guid.Empty,
            firstName: "",
            lastName: "",
            email: "",
            password: "");

}