namespace StorEsc.Domain.Entities;

public class Seller : Account
{
    public Guid WalletId { get; private set; }
    
    // EF
    public Wallet Wallet { get; private set; }
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
        : base(id, firstName, lastName, email, password)
    {
        WalletId = walletId;
        
        Validate();
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
        : base(id, firstName, lastName, email, password)
    {
        WalletId = walletId;
        Wallet = wallet;
        Products = products;
        
        Validate();
    }
    
    public override void SetWallet(Wallet wallet)
    {
        WalletId = wallet.Id;
        Wallet = wallet;
        
        Validate();
    }
    
    public void Validate()
        => base.Validate();
}