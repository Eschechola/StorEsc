namespace StorEsc.Domain.Entities;

public class Seller : Account
{
    // EF
    public IList<Product> Products { get; private set; }

    protected Seller() { }
    
    public Seller(
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(walletId, firstName, lastName, email, password)
    {
    }
    
    public Seller(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(id, walletId, firstName, lastName, email, password)
    {
    }

    public Seller(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password,
        List<Product> products = null,
        Wallet wallet = null) 
        : base(id, walletId, firstName, lastName, email, password, wallet)
    {
        Products = products;
    }
    
    public void Validate()
        => base.Validate();
}