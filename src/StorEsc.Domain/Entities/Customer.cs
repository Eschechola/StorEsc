namespace StorEsc.Domain.Entities;

public class Customer : Account
{
    // EF
    public List<Order> Orders { get; private set; }
    
    
    protected Customer()
    {
    }
    
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
        List<Order> orders = null) 
        : base(id, walletId, firstName, lastName, email, password)
    {
        Orders = orders;
    }
}