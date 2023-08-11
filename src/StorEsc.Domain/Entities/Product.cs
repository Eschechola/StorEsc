using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Guid SellerId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public bool Enabled { get; private set; }
    
    //EF
    public Seller Seller { get; private set; }
    public List<OrderItem> OrderItens { get; private set; }

    protected Product() { }
    
    public Product(
        Guid sellerId,
        string name,
        string description,
        decimal price,
        int stock,
        bool enabled)
    {
        SellerId = sellerId;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Enabled = enabled;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new ProductValidator(), this);

    public void SetName(string name)
    {
        Name = name;
        Validate();
    }

    public void SetDescription(string description)
    {
        Description = description;
        Validate();
    }

    public void SetPrice(decimal price)
    {
        Price = price;
        Validate();
    }
    
    public void SetStock(int stock)
    {
        Stock = stock;
        Validate();
    }

    public void Disable() 
        => Enabled = false;
    
    public void Enable() 
        => Enabled = true;
}