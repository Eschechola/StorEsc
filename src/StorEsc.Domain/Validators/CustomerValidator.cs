using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        
    }
}