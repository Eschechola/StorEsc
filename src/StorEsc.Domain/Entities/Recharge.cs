using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Recharge : Entity, IAggregateRoot
{
    public Guid WalletId { get; private set; }
    public decimal Amount { get; private set; }
    public string PaymentHash { get; private set; }
    
    //EF
    public Wallet Wallet { get; private set; }

    protected Recharge() { }
    
    public Recharge(
        Guid walletId,
        decimal amount,
        string paymentHash)
    {
        WalletId = walletId;
        Amount = amount;
        PaymentHash = paymentHash;
    }

    public Recharge(
        Guid id,
        Guid walletId,
        decimal amount,
        string paymentHash,
        Wallet wallet) : base(id)
    {
        WalletId = walletId;
        Amount = amount;
        PaymentHash = paymentHash;
        Wallet = wallet;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new RechargeValidator(), this);
}