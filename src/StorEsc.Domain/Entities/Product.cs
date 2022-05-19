using StorEsc.Domain.Interfaces;

namespace StorEsc.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Guid SellerId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }

    protected Product() { }
    
    public Product(
        Guid sellerId,
        string name,
        string description,
        double price)
    {
        SellerId = sellerId;
        Name = name;
        Description = description;
        Price = price;
        
        Validate();
    }

    public Product(
        Guid id,
        Guid sellerId,
        string name,
        string description,
        double price) : base(id)
    {
        SellerId = sellerId;
        Name = name;
        Description = description;
        Price = price;
        
        Validate();
    }
    
    public void Validate(){}
}