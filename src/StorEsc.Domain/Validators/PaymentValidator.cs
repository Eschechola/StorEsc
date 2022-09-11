using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(x => x.IsPaid)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("IsPaid"));
    }
}