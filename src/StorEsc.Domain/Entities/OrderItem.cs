namespace StorEsc.Domain.Entities;

public class OrderItem : Entity
{
    public int Count { get; set; }
    public Product Product { get; private set; }
    
    protected OrderItem() { }
    
    public OrderItem(
        int count,
        Product product)
    {
        Count = count;
        Product = product;
    }
    
    public OrderItem(
        Guid id,
        int count,
        Product product)
        : base (id)
    {
        Count = count;
        Product = product;
    }

    public double CalculateItemValue()
        => Product.Price * Count;
}