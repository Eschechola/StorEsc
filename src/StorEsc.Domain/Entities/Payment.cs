using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Payment : Entity
{
    public string Hash { get; private set; }
    public bool IsPaid { get; private set; }
    
    // EF
    public IList<Recharge> Recharges { get; private set; }

    public Payment(
        Guid id,
        string hash,
        bool isPaid,
        DateTime createdAt,
        DateTime updatedAt,
        IList<Recharge> recharges) : base(id, createdAt, updatedAt)
    {
        Hash = hash;
        IsPaid = isPaid;
        Recharges = recharges;
        
        Validate();
    }
    
    public Payment(bool isPaid)
    {
        IsPaid = isPaid;
        Hash = string.Empty;
        
        Validate();
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