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
        : base(firstName, lastName, email, password)
    {
        WalletId = walletId;
    }

    public void SetWallet(Wallet wallet)
    {
        WalletId = wallet.Id;
        Wallet = wallet;
        
        Validate();
    }
}