using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(property => property.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}