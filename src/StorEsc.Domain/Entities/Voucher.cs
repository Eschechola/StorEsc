using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Voucher : Entity, IAggregateRoot
{
    public string Code { get; private set; }
    public decimal? ValueDiscount { get; private set; }
    public decimal? PercentageDiscount { get; private set; }
    public bool IsPercentageDiscount { get; private set; }
    
    //EF
    public List<Order> Orders { get; private set; }

    protected Voucher() { }
    
    public Voucher(
        string code,
        decimal? valueDiscount,
        decimal percentageDiscount,
        bool isPercentageDiscount)
    {
        Code = code;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        IsPercentageDiscount = isPercentageDiscount;
        
        Validate();
    }

    public Voucher(
        Guid id,
        string code,
        decimal? valueDiscount,
        decimal percentageDiscount,
        bool isPercentageDiscount) 
        : base(id)
    {
        Code = code;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        IsPercentageDiscount = isPercentageDiscount;
        
        Validate();
    }
    
    public Voucher(
        Guid id,
        string code,
        decimal? valueDiscount,
        decimal percentageDiscount,
        bool isPercentageDiscount,
        List<Order> orders) 
        : base(id)
    {
        Code = code;
        ValueDiscount = valueDiscount;
        PercentageDiscount = percentageDiscount;
        IsPercentageDiscount = isPercentageDiscount;
        Orders = orders;
        
        Validate();
    }


    public void Validate()
        => base.Validate(new VoucherValidator(), this);
}