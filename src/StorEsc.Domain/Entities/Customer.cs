namespace StorEsc.Domain.Entities;

public class Customer : Account
{
    // EF
    public List<Order> Orders { get; private set; }
    public Wallet Wallet { get; private set; }
    
    protected Customer() { }
    
    public Customer(
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(walletId, firstName, lastName, email, password)
    {
    }
    
    public Customer(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(id, walletId, firstName, lastName, email, password)
    {
    }

    public Customer(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password,
        List<Order> orders = null,
        Wallet wallet = null) 
        : base(id, walletId, firstName, lastName, email, password)
    {
        Orders = orders;
        Wallet = wallet;
    }
}