using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public abstract class Account : Entity, IAggregateRoot
{
    public Guid WalletId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Wallet Wallet { get; private set; }
    
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
        string password,
        Wallet wallet = null)
        : base (id)
    {
        WalletId = walletId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Wallet = wallet;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new AccountValidator(), this);

    public void SetPassword(string password)
        => Password = password;

    public void SetWallet(Wallet wallet)
    {
        WalletId = wallet.Id;
        Wallet = wallet;
    }
}