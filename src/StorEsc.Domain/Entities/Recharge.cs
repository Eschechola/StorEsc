using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Recharge : Entity, IAggregateRoot
{
    public Guid WalletId { get; private set; }
    public Guid PaymentId { get; private set; }
    public double Amount { get; private set; }

    //EF
    public Wallet Wallet { get; private set; }
    public Payment Payment { get; private set; }

    protected Recharge() { }
    
    public Recharge(
        Guid walletId,
        Guid paymentId,
        double amount)
    {
        WalletId = walletId;
        PaymentId = paymentId;
        Amount = amount;
    }

    public Recharge(
        Guid id,
        Guid paymentId,
        Guid walletId,
        double amount,
        Wallet wallet,
        Payment payment) : base(id)
    {
        WalletId = walletId;
        PaymentId = paymentId;
        Amount = amount;
        Wallet = wallet;
        Payment = payment;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new RechargeValidator(), this);
}