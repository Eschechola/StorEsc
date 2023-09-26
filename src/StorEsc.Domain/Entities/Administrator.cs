namespace StorEsc.Domain.Entities;

public class Administrator : Account
{
    public Guid CreatedBy { get; private set; }
    public bool IsEnabled { get; private set; }
    
    protected Administrator() { }
    
    public Administrator(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password,
        Guid createdBy,
        DateTime createdAt,
        DateTime updatedAt,
        bool isEnabled = false) 
        : base(id, firstName, lastName, email, password, createdAt, updatedAt)
    {
        CreatedBy = createdBy;
        IsEnabled = isEnabled;
        
        Validate();
    }
    
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

    public void Disable()
        => IsEnabled = false;
}