namespace StorEsc.Application.Dtos;

public record RechargeDto : BaseDto
{
    public Guid WalletId { get; init; }
    public decimal Amount { get; init; }
}