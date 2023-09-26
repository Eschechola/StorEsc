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
        Guid id,
        int itemCount,
        DateTime createdAt,
        DateTime updatedAt,
        Product product,
        Order order) : base(id, createdAt, updatedAt)
    {
        ItemCount = itemCount;
        Product = product;
        Order = order;
    }
    
    public OrderItem(
        Guid orderId,
        Guid productId,
        int itemCount)
    {
        OrderId = orderId;
        ProductId = productId;
        ItemCount = itemCount;
    }

    public decimal CalculateItemValue()
        => Product.Price * ItemCount;
}