using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(property => property.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);
        
        RuleFor(property => property.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(100)
            .MaximumLength(2000);

        RuleFor(property => property.Price)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(5);
        
        RuleFor(property=>property.Stock)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}