using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(property => property.IsPaid)
            .NotNull();
    }
}