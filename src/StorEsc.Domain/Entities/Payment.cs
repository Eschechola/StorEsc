using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Payment : Entity
{
    public string Hash { get; private set; }
    public bool IsPaid { get; private set; }
    
    // EF
    public IList<Recharge> Recharges { get; private set; }

    public Payment(bool isPaid)
    {
        IsPaid = isPaid;
        Hash = string.Empty;
    }
    
    public Payment(
        string hash,
        bool isPaid)
    {
        Hash = hash;
        IsPaid = isPaid;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new PaymentValidator(), this);
}