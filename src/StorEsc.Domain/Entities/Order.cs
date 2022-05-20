using StorEsc.Domain.Interfaces;

namespace StorEsc.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public double TotalValue { get { return CalculateOrderPrice(); } }
    public List<OrderItem> Items { get; private set; }
    
    //EF
    public Customer Customer { get; private set; }

    protected Order() { }
    
    public Order(
        Guid customerId,
        List<OrderItem> items)
    {
        CustomerId = customerId;
        Items = items;
        
        Validate();
    }
    
    public Order(
        Guid id,
        Guid customerId,
        List<OrderItem> items,
        Customer customer = null)
        : base(id)
    {
        CustomerId = customerId;
        Items = items;
        Customer = customer;
        
        Validate();
    }
    
    private double CalculateOrderPrice()
    {
        double totalValue = 0;
        
        foreach (var item in Items)
            totalValue += item.CalculateItemValue();

        return totalValue;
    }
    
    public void Validate(){}
}