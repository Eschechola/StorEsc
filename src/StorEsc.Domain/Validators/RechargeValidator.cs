using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class RechargeValidator : AbstractValidator<Recharge>
{
    public RechargeValidator()
    {
        RuleFor(property => property.WalletId)
            .NotNull()
            .NotEmpty();

        RuleFor(property => property.Amount)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(10);
    }
}