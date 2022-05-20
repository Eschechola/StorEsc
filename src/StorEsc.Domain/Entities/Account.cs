using StorEsc.Domain.Interfaces;

namespace StorEsc.Domain.Entities;

public abstract class Account : Entity, IAggregateRoot
{
    public Guid WalletId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    
    protected Account() { }
    
    public Account(
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password)
    {
        WalletId = walletId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        
        Validate();
    }
    
    public Account(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password)
        : base (id)
    {
        WalletId = walletId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        
        Validate();
    }

    public void Validate(){}
}