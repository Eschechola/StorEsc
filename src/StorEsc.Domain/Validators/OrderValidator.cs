using FluentValidation;
using StorEsc.Core.Messages;
using StorEsc.Domain.Entities;

namespace StorEsc.Domain.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("CustomerId"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("CustomerId"));
        
        RuleFor(x => x.TotalValue)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("TotalValue"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("TotalValue"));
        
        RuleFor(x => x.IsPaid)
            .NotNull()
            .WithMessage(ValidatorMessages.NotNull("IsPaid"))

            .NotEmpty()
            .WithMessage(ValidatorMessages.NotEmpty("IsPaid"));
    }
}