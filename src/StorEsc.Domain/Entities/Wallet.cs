using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Wallet : Entity, IAggregateRoot
{
    public decimal Amount { get; private set; }

    //EF
    public IList<Customer> Customers { get; private set; }
    public IList<Seller> Sellers { get; private set; }
    public List<Recharge> Recharges { get; private set; }

    
    protected Wallet() { }
    
    public Wallet(decimal amount)
    {
        Amount = amount;
        
        Validate();
    }
    
    public Wallet(
        Guid id,
        decimal amount) 
        : base(id)
    {
        Amount = amount;
        
        Validate();
    }
    
    public void Validate()
        => base.Validate(new WalletValidator(), this);

    public void DebitAmount(decimal amount)
    {
        if (Amount < amount)
            throw new Exception("The user doesn't has correct currency");

        Amount -= amount;
    }

    public void AddAmount(decimal amount)
        => Amount += amount;
}