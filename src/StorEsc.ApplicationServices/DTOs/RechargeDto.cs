namespace StorEsc.Application.Dtos;

public class RechargeDto : BaseDto
{
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
}