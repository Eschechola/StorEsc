namespace StorEsc.Application.Dtos;

public record CustomerDto : AccountDto
{
    public Guid WalletId { get; init; }
    public WalletDto Wallet { get; init; }
    public IList<OrderDto> Orders { get; init; }
}