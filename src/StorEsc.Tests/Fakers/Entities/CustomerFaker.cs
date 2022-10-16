using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class CustomerFaker : IFaker<Customer>
{
    private readonly Person _personFaker;
    private readonly Internet _internetFaker;

    public CustomerFaker()
    {
        _personFaker = new Person();
        _internetFaker = new Internet();
    }
    
    public Customer GetValid()
        => new Customer(
            id: Guid.NewGuid(),
            walletId: Guid.NewGuid(),
            firstName: _personFaker.FirstName,
            lastName: _personFaker.LastName,
            email: _personFaker.Email,
            password: _internetFaker.Password());

    public Customer GetInvalid()
        => new Customer(
            id: Guid.Empty,
            walletId: Guid.Empty,
            firstName: "",
            lastName: "",
            email: "",
            password: "");

}