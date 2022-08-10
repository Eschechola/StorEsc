using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Guid SellerId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    
    //EF
    public Seller Seller { get; private set; }
    public List<OrderItem> OrderItens { get; private set; }

    protected Product() { }
    
    public Product(
        Guid sellerId,
        string name,
        string description,
        decimal price)
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
        decimal price,
        Seller seller = null,
        List<OrderItem> orderItens = null) : base(id)
    {
        SellerId = sellerId;
        Name = name;
        Description = description;
        Price = price;
        Seller = seller;
        OrderItens = orderItens;
        
        Validate();
    }

    public void Validate()
        => base.Validate(new ProductValidator(), this);
}