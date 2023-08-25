using FluentValidation;
using FluentValidation.Results;

namespace StorEsc.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    private List<string> _errors { get; set;  }
    
    public IReadOnlyCollection<string> Errors => _errors;

    public Entity()
    {
        _errors = new List<string>();
    }
    
    public Entity(Guid id)
    {
        Id = id;
        _errors = new List<string>();
    }
    
    public string ErrorsToString()
    {
        string errors = "";

        foreach (var error in _errors)
            errors += error + " ";

        return errors;
    }

    public bool IsInvalid()
        => _errors.Count > 0;
    
    public void CreatedAtNow()
        => CreatedAt = DateTime.UtcNow;
    
    public void UpdatedAtNow()
        => UpdatedAt = DateTime.UtcNow;
       
    protected void Validate<V, O>(V validator, O obj) 
        where V : AbstractValidator<O>
    {
        ClearErrors();
        var validation = validator.Validate(obj);

        if (validation.Errors.Count > 0)
            AddErrorList(validation.Errors);
    }

    private void AddErrorList(List<ValidationFailure> errors)
    {
        foreach(var error in errors)
            _errors.Add(error.ErrorMessage);
    }

    private void ClearErrors()
        => _errors.Clear();
}