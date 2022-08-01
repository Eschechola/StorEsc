namespace StorEsc.Application.DTOs;

public class VoucherDTO : BaseDTO
{
    public string Code { get; set; }
    public decimal? ValueDiscount { get; set; }
    public decimal? PercentageDiscount { get; set; }
    public bool IsPercentageDiscount { get; set; }
}