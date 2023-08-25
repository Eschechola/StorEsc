using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class VoucherValidator : AbstractValidator<Voucher>
{
    public VoucherValidator()
    {
        RuleFor(x=>x.Code)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("Code"))
                        
            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("Code"))
                        
            .MinimumLength(3)
            .WithMessage(ValidatorMessages.MinimumLength("Code", 3))
                        
            .MaximumLength(80)
            .WithMessage(ValidatorMessages.MaximumLength("Code", 80));

        RuleFor(x => x.IsPercentageDiscount)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("IsPercentageDiscount"));
    }
}