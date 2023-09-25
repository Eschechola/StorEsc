using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public abstract class Account : Entity, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    protected Account() { }
    
    protected Account(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdAt,
        DateTime updatedAt) : base (id, createdAt, updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        
        Validate();
    }
    
    protected Account(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new AccountValidator(), this);

    public void SetPassword(string password)
        => Password = password;
}