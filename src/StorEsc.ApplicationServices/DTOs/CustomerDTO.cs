namespace StorEsc.Application.DTOs;

public class CustomerDTO : AccountDTO
{
    public Guid WalletId { get; set; }
    public WalletDTO Wallet { get; set; }
    public IList<OrderDTO> Orders { get; set; }

    public CustomerDTO()
    {
        Orders = new List<OrderDTO>();
    }
}