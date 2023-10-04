using StorEsc.Domain.Validators;

namespace StorEsc.Domain.Entities;

public class Category : Entity
{
    public string Name { get; private set; }
    
    // EF
    public IList<Product> Products { get; private set; }

    public Category(string name)
    {
        Name = name;
        
        Validate();
    }

    public Category(
        Guid id,
        string name,
        DateTime createdAt,
        DateTime updatedAt, 
        IList<Product> products) : base(id, createdAt, updatedAt)
    {
        Name = name;
        Products = products;
        
        Validate();
    }

    private void Validate()
        => base.Validate(new CategoryValidator(), this);
}