﻿namespace StorEsc.Domain.Entities;

public class Customer : Account
{
    public Guid WalletId { get; private set; }
    
    // EF
    public Wallet Wallet { get; private set; }
    public IList<Order> Orders { get; private set; }
    
    protected Customer() { }
    
    public Customer(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdAt,
        DateTime updatedAt,
        Wallet wallet,
        IList<Order> orders) 
        : base(id, firstName, lastName, email, password, createdAt, updatedAt)
    {
        WalletId = walletId;
        Wallet = wallet;
        Orders = orders;
        
        Validate();
    }
    
    public Customer(
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(firstName, lastName, email, password)
    {
        WalletId = walletId;
        
        Validate();
    }

    public void SetWallet(Wallet wallet)
    {
        WalletId = wallet.Id;
        Wallet = wallet;
    }
}