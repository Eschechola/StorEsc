using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(property=>property.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        
        RuleFor(property=>property.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        
        RuleFor(property=>property.Email)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(200)
            .EmailAddress();

        RuleFor(property => property.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(120);
    }
}