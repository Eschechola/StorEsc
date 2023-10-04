using FluentValidation;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class VoucherValidator : AbstractValidator<Voucher>
{
    public VoucherValidator()
    {
        RuleFor(property=>property.Code)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(80);

        RuleFor(property => property.IsPercentageDiscount)
            .NotNull();
    }
}