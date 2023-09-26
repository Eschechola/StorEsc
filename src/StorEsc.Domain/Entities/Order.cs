using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public decimal TotalValue { get; private set;  }
    public bool IsPaid { get; private set; }

    //EF
    public Customer Customer { get; private set; }
    public Voucher Voucher { get; private set; }
    public IList<OrderItem> OrderItens { get; private set; }

    protected Order() { }
    
    public Order(
        Guid id,
        Guid customerId,
        bool isPaid,
        DateTime createdAt,
        DateTime updatedAt,
        Customer customer,
        Voucher voucher,
        IList<OrderItem> orderItens) : base(id, createdAt, updatedAt)
    {
        CustomerId = customerId;
        IsPaid = isPaid;
        Customer = customer;
        Voucher = voucher;
        OrderItens = orderItens;
        TotalValue = CalculateOrderPrice();
        
        Validate();
    }
    
    public Order(
        Guid customerId,
        bool isPaid)
    {
        CustomerId = customerId;
        IsPaid = isPaid;
        TotalValue = CalculateOrderPrice();
        
        Validate();
    }
    
    public void Validate()
        => base.Validate(new OrderValidator(), this);

    public void PayOrder()
        => IsPaid = true;

    public void SetVoucher(Voucher voucher)
        => Voucher = voucher;
    
    private decimal CalculateOrderPrice()
    {
        decimal totalValue = 0;
        
        foreach (var item in OrderItens)
            totalValue += item.CalculateItemValue();

        if (string.IsNullOrEmpty(Voucher.Code))
            return totalValue;

        if (Voucher.IsPercentageDiscount)
           return totalValue - (totalValue * (Voucher.PercentageDiscount.Value / 100));
        
        return totalValue - Voucher.ValueDiscount.Value;
    }
}