namespace StorEsc.Application.DTOs;

public class OrderItemDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ItemCount { get; set; }
}