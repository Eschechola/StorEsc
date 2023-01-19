using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class CustomerFaker : BaseFaker<Customer>
{
    private readonly Person _personFaker;
    private readonly Internet _internetFaker;

    public CustomerFaker()
    {
        _personFaker = new Person();
        _internetFaker = new Internet();
    }
    
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