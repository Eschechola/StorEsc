namespace StorEsc.Domain.Entities;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int ItemCount { get; private set; }
    
    // EF
    public Order Order { get; private set; }
    public Product Product { get; private set; }
    
    protected OrderItem() { }
    
    public OrderItem(
        int itemCount,
        Product product,
        Order order)
    {
        ItemCount = itemCount;
        Product = product;
        Order = order;
    }
    
    public OrderItem(
        Guid id,
        int itemCount,
        Product product,
        Order order)
        : base (id)
    {
        ItemCount = itemCount;
        Product = product;
        Order = order;
    }

    public decimal CalculateItemValue()
        => Product.Price * ItemCount;
}