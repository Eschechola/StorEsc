namespace StorEsc.Application.Dtos;

public class VoucherDto : BaseDto
{
    public string Code { get; set; }
    public decimal? ValueDiscount { get; set; }
    public decimal? PercentageDiscount { get; set; }
    public bool IsPercentageDiscount { get; set; }
}