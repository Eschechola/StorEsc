using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Voucher : Entity, IAggregateRoot
{
    public Guid SellerId { get; private set; }
    public string Code { get; private set; }
    public decimal? ValueDiscount { get; private set; }
    public decimal? PercentageDiscount { get; private set; }
    public bool IsPercentageDiscount { get; private set; }
    public bool Enabled { get; private set; }
    
    //EF
    public List<Order> Orders { get; private set; }
    public Seller Seller { get; private set; }

    protected Voucher() { }
    
    public Voucher(
        Guid sellerId,
        string code,
        decimal? valueDiscount,
        decimal? percentageDiscount,
        bool isPercentageDiscount,
        bool enabled)
    {
        SellerId = sellerId;
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

    public void SetSellerId(string sellerId)
        => SellerId = Guid.Parse(sellerId);

    public void CodeToUpper()
        => Code = Code.ToUpper();

    public void SetDiscounts()
    {
        ValueDiscount = IsPercentageDiscount ? null : ValueDiscount;
        PercentageDiscount = IsPercentageDiscount ? PercentageDiscount : null;
    }
}