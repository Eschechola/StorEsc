using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class WalletValidator : AbstractValidator<Wallet>
{
    public WalletValidator()
    {
        RuleFor(property => property.Amount)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}