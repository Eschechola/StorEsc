using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class RechargeValidator : AbstractValidator<Recharge>
{
    public RechargeValidator()
    {
        RuleFor(x => x.WalletId)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("WalletId"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("WalletId"));
        
        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Amount"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Amount"))
            
            .GreaterThanOrEqualTo(10)
            .WithMessage(ValidatorMessages.GreaterThanOrEqualTo("Amount", 10));
    }
}