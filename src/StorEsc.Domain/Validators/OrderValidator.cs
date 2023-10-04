using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(property => property.CustomerId)
            .NotNull()
            .NotEmpty();

        RuleFor(property => property.TotalValue)
            .NotNull()
            .NotEmpty();

        RuleFor(property => property.IsPaid)
            .NotNull()
            .NotEmpty();
    }
}