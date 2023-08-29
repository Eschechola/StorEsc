namespace StorEsc.Application.Dtos;

public class CustomerDto : AccountDto
{
    public Guid WalletId { get; set; }
    public WalletDto Wallet { get; set; }
    public IList<OrderDto> Orders { get; set; }

    public CustomerDto()
    {
        Orders = new List<OrderDto>();
    }
}