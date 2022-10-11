namespace StorEsc.Domain.Entities;

public class Administrator : Account
{
    protected Administrator() { }
    
    public Administrator(
        Guid walletId,
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(walletId, firstName, lastName, email, password)
    {
        Validate();
    }

    public void Valitade()
        => base.Validate();
}