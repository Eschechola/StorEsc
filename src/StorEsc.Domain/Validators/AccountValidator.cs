using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x=>x.FirstName)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("FirstName"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("FirstName"))
            
            .MinimumLength(3)
            .WithMessage(ValidatorMessages.MinimumLength("FirstName", 3))
            
            .MaximumLength(100)
            .WithMessage(ValidatorMessages.MaximumLength("FirstName", 100));
        
        RuleFor(x=>x.LastName)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("LastName"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("LastName"))
            
            .MinimumLength(3)
            .WithMessage(ValidatorMessages.MinimumLength("LastName", 3))
            
            .MaximumLength(100)
            .WithMessage(ValidatorMessages.MaximumLength("LastName", 100));
        
        RuleFor(x=>x.Email)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Email"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Email"))
            
            .MinimumLength(10)
            .WithMessage(ValidatorMessages.MinimumLength("Email", 10))
            
            .MaximumLength(200)
            .WithMessage(ValidatorMessages.MaximumLength("Email", 200))
            
            .EmailAddress()
            .WithMessage(ValidatorMessages.InvalidProperty("Email"));

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Password"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Password"))

            .MinimumLength(6)
            .WithMessage(ValidatorMessages.MinimumLength("Password", 6))

            .MaximumLength(120)
            .WithMessage(ValidatorMessages.MaximumLength("Password", 120));
    }
}