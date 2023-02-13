namespace StorEsc.Domain.Entities;

public class Administrator : Account
{
    public Guid CreatedBy { get; private set; }
    public bool IsEnabled { get; private set; }
    
    protected Administrator() { }
    
    public Administrator(
        string firstName,
        string lastName,
        string email,
        string password,
        Guid createdBy,
        bool isEnabled = false) 
        : base(firstName, lastName, email, password)
    {
        CreatedBy = createdBy;
        IsEnabled = isEnabled;
        
        Validate();
    }

    public void Enable()
        => IsEnabled = true;
}