namespace StorEsc.Application.DTOs;

public class VoucherDTO
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public decimal? ValueDiscount { get; set; }
    public decimal? PercentageDiscount { get; set; }
    public bool IsPercentageDiscount { get; set; }
}