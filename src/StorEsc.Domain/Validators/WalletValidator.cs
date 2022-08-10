using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class WalletValidator : AbstractValidator<Wallet>
{
    public WalletValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Amount"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Amount"))

            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidatorMessages.GreaterThanOrEqualTo("Amount", 0));
    }
}