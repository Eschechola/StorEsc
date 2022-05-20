using StorEsc.Domain.Interfaces;

namespace StorEsc.Domain.Entities;

public class Recharge : Entity, IAggregateRoot
{
    public Guid WalletId { get; private set; }
    public decimal Amount { get; private set; }
    
    //EF
    public Wallet Wallet { get; private set; }

    protected Recharge() { }
    
    public Recharge(
        Guid walletId,
        decimal amount)
    {
        WalletId = walletId;
        Amount = amount;
    }

    public Recharge(
        Guid id,
        Guid walletId,
        decimal amount,
        Wallet wallet) : base(id)
    {
        WalletId = walletId;
        Amount = amount;
        Wallet = wallet;
        
        Validate();
    }

    public void Validate()
    {
    }
}