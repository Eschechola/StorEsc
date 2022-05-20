﻿using StorEsc.Domain.Interfaces;

namespace StorEsc.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public decimal TotalValue { get; private set;  }
    public bool IsPaid { get; private set; }

    //EF
    public Customer Customer { get; private set; }
    public List<OrderItem> OrderItens { get; private set; }

    protected Order() { }
    
    public Order(
        Guid customerId,
        bool isPaid,
        List<OrderItem> orderItens = null)
    {
        CustomerId = customerId;
        IsPaid = isPaid;
        OrderItens = orderItens;
        TotalValue = CalculateOrderPrice();
        
        Validate();
    }
    
    public Order(
        Guid id,
        Guid customerId,
        bool isPaid,
        List<OrderItem> orderItens = null,
        Customer customer = null)
        : base(id)
    {
        CustomerId = customerId;
        IsPaid = isPaid;
        OrderItens = orderItens;
        Customer = customer;
        TotalValue = CalculateOrderPrice();
        
        Validate();
    }

    public void PayOrder()
        => IsPaid = true;
    
    private decimal CalculateOrderPrice()
    {
        decimal totalValue = 0;
        
        foreach (var item in OrderItens)
            totalValue += item.CalculateItemValue();

        return totalValue;
    }
    
    public void Validate(){}
}