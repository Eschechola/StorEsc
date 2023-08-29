namespace StorEsc.Application.Dtos;

public record WalletDto : BaseDto
{
    public decimal Amount { get; init; }
}