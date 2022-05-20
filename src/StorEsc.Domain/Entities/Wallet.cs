using StorEsc.Domain.Interfaces;

namespace StorEsc.Domain.Entities;

public class Wallet : Entity, IAggregateRoot
{
    public double Amount { get; private set; }

    //EF
    public IList<Customer> Customers { get; private set; }
    public IList<Seller> Sellers { get; private set; }

    
    protected Wallet() { }
    
    public Wallet(double amount)
    {
        Amount = amount;
        
        Validate();
    }
    
    public Wallet(
        Guid id,
        double amount) 
        : base(id)
    {
        Amount = amount;
        
        Validate();
    }
    
    public Wallet(
        Guid id,
        double amount,
        IList<Customer> customers = null,
        IList<Seller> sellers = null) 
        : base(id)
    {
        Amount = amount;
        Customers = customers;
        Sellers = sellers;
        
        Validate();
    }

    public void DebitAmount(double amount)
    {
        if (Amount < amount)
            throw new Exception("The user doesn't has correct currency");

        Amount -= amount;
    }

    public void AddAmount(double amount)
        => Amount += amount;
    
    public void Validate(){}
}