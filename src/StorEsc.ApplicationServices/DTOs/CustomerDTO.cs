namespace StorEsc.Application.DTOs;

public class CustomerDTO : AccountDTO
{
    public IList<OrderDTO> Orders { get; set; }

    public CustomerDTO()
    {
        Orders = new List<OrderDTO>();
    }
}