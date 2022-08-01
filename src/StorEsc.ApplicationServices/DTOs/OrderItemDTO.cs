namespace StorEsc.Application.DTOs;

public class OrderItemDTO : BaseDTO
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ItemCount { get; set; }
}