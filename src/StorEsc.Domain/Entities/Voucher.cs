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
    public IList<Order> Orders { get; private set; }

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
        SetDiscounts();
        CodeToUpper();
    }
    
    public Voucher(
        Guid id,
        string code,
        decimal? valueDiscount,
        decimal? percentageDiscount,
        bool isPercentageDiscount,
        bool enabled,
        DateTime createdAt,
        DateTime updatedAt,
        IList<Order> orders) : base(id, createdAt, updatedAt)
    {
        Code = code;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        IsPercentageDiscount = isPercentageDiscount;
        Enabled = enabled;
        Orders = orders;
        
        Validate();
        SetDiscounts();
        CodeToUpper();
    }
    
    public void Validate()
        => base.Validate(new VoucherValidator(), this);

    public void Enable()
        => Enabled = true;

    public void Disable()
        => Enabled = false;
    
    public void SetCode(string code)
        => Code = code;
    
    public void SetDiscounts(
        bool isPercentageDiscount,
        decimal? valueDiscount = null,
        decimal? percentageDiscount = null)
    {
        IsPercentageDiscount = isPercentageDiscount;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        
        SetDiscounts();
    }

    private void CodeToUpper()
        => Code = Code.ToUpper();
    
    private void SetDiscounts()
    {
        ValueDiscount = IsPercentageDiscount ? null : ValueDiscount;
        PercentageDiscount = IsPercentageDiscount ? PercentageDiscount : null;
    }
}