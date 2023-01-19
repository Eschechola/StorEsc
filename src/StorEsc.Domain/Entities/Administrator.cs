namespace StorEsc.Domain.Entities;

public class Administrator : Account
{
    protected Administrator() { }
    
    public Administrator(
        string firstName,
        string lastName,
        string email,
        string password) 
        : base(firstName, lastName, email, password)
    {
        Validate();
    }
}