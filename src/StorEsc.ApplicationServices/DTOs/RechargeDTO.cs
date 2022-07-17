namespace StorEsc.Application.DTOs;

public class RechargeDTO
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
}