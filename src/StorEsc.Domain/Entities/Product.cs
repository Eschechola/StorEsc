﻿using StorEsc.Domain.Interfaces;
using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public bool Enabled { get; private set; }
    
    //EF
    public IList<OrderItem> OrderItens { get; private set; }
    public Category Category { get; private set; }

    protected Product() { }
    
    public Product(
        Guid categoryId,
        string name,
        string description,
        decimal price,
        int stock,
        bool enabled)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Enabled = enabled;
        
        Validate();
    }
    
    public Product(
        Guid id,
        Guid categoryId,
        string name,
        string description,
        decimal price,
        int stock,
        bool enabled,
        DateTime createdAt,
        DateTime updatedAt,
        IList<OrderItem> orderItems,
        Category category): base(id, createdAt, updatedAt)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Enabled = enabled;
        OrderItens = orderItems;
        Category = category;
        
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