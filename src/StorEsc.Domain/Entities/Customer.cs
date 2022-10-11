namespace StorEsc.Domain.Entities;

public class Customer : Account
{
    public Guid WalletId { get; private set; }
    
    // EF
    public Wallet Wallet { get; private set; }
    public IList<Order> Orders { get; private set; }
    
    protected Customer() { }
    
    public Customer(
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(walletId, firstName, lastName, email, password)
    {
        WalletId = walletId;
        
        Validate();
    }
    
    public Customer(
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

    public Customer(
        Guid id,
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password,
        List<Order> orders = null,
        Wallet wallet = null) 
        : base(id, firstName, lastName, email, password)
    {
        Orders = orders;
        WalletId = walletId;
        Wallet = wallet;
        
        Validate();
    }
    
    public void Validate()
        => base.Validate();
    
    public void SetWallet(Wallet wallet)
    {
        WalletId = wallet.Id;
        Wallet = wallet;
    }
}