using Bogus;
using Bogus.DataSets;
using StorEsc.Domain.Entities;

namespace StorEsc.Tests.Fakers.Entities;

public class AdministratorFaker : BaseFaker<Administrator>
{
    private readonly Person _personFaker;
    private readonly Internet _internetFaker;

    public AdministratorFaker()
    {
        _personFaker = new Person();
        _internetFaker = new Internet();
    }
    
    public override Administrator GetValid()
        => new Administrator(
            firstName: _personFaker.FirstName,
            lastName: _personFaker.LastName,
            email: _personFaker.Email,
            password: _internetFaker.Password(),
            createdBy: Guid.NewGuid(),
            isEnabled: true);

    public override Administrator GetInvalid()
        => new Administrator(
            firstName: "",
            lastName: "",
            email: "",
            password: "",
            createdBy: Guid.Empty,
            isEnabled: true);
}