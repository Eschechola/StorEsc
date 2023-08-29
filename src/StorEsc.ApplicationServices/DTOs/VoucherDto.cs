namespace StorEsc.Application.Dtos;

public record VoucherDto : BaseDto
{
    public string Code { get; init; }
    public decimal? ValueDiscount { get; init; }
    public decimal? PercentageDiscount { get; init; }
    public bool IsPercentageDiscount { get; init; }
    public bool Enabled { get; init; }
}