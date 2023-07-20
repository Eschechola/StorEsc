namespace StorEsc.Application.Dtos;

public class OrderItemDto : BaseDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ItemCount { get; set; }
}