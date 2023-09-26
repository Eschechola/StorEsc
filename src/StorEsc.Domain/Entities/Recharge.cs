using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Recharge : Entity, IAggregateRoot
{
    public Guid WalletId { get; private set; }
    public Guid PaymentId { get; private set; }
    public decimal Amount { get; private set; }

    //EF
    public Wallet Wallet { get; private set; }
    public Payment Payment { get; private set; }

    protected Recharge() { }
    
    public Recharge(
        Guid id,
        Guid walletId,
        Guid paymentId,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        Wallet wallet,
        Payment payment) : base(id, createdAt, updatedAt)
    {
        WalletId = walletId;
        PaymentId = paymentId;
        Amount = amount;
        Wallet = wallet;
        Payment = payment;
    }
    
    public Recharge(
        Guid walletId,
        Guid paymentId,
        decimal amount)
    {
        WalletId = walletId;
        PaymentId = paymentId;
        Amount = amount;
    }

    public void Validate()
        => base.Validate(new RechargeValidator(), this);
}