namespace StorEsc.Application.DTOs;

public class RechargeDTO : BaseDTO
{
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
}