using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Name"))
            
            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Name"))
            
            .MinimumLength(3)
            .WithMessage(ValidatorMessages.MinimumLength("Name", 3))
            
            .MaximumLength(200)
            .WithMessage(ValidatorMessages.MaximumLength("Name", 200));
        
        RuleFor(x => x.Description)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Description"))
            
            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Description"))

            .MinimumLength(100)
            .WithMessage(ValidatorMessages.MinimumLength("Description", 100))
            
            .MaximumLength(2000)
            .WithMessage(ValidatorMessages.MaximumLength("Description", 2000));

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Price"))
            
            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Price"))

            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidatorMessages.GreaterThanOrEqualTo("Price", 1));
    }
}