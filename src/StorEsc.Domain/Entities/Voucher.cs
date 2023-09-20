using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Voucher : Entity, IAggregateRoot
{
    public string Code { get; private set; }
    public decimal? ValueDiscount { get; private set; }
    public decimal? PercentageDiscount { get; private set; }
    public bool IsPercentageDiscount { get; private set; }
    public bool Enabled { get; private set; }
    
    //EF
    public List<Order> Orders { get; private set; }

    protected Voucher() { }
    
    public Voucher(
        string code,
        decimal? valueDiscount,
        decimal? percentageDiscount,
        bool isPercentageDiscount,
        bool enabled)
    {
        Code = code;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        IsPercentageDiscount = isPercentageDiscount;
        Enabled = enabled;
        
        Validate();
    }
    
    public Voucher(
        Guid id,
        string code,
        decimal? valueDiscount,
        decimal? percentageDiscount,
        bool isPercentageDiscount,
        bool enabled,
        DateTime createdAt,
        DateTime updatedAt) : base(id, createdAt, updatedAt)
    {
        Code = code;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        IsPercentageDiscount = isPercentageDiscount;
        Enabled = enabled;
        
        Validate();
    }
    
    public void Validate()
        => base.Validate(new VoucherValidator(), this);

    public void Enable()
        => Enabled = true;

    public void Disable()
        => Enabled = false;

    public void CodeToUpper()
        => Code = Code.ToUpper();

    public void SetDiscounts()
    {
        ValueDiscount = IsPercentageDiscount ? null : ValueDiscount;
        PercentageDiscount = IsPercentageDiscount ? PercentageDiscount : null;
    }
}