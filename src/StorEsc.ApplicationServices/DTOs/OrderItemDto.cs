namespace StorEsc.Application.Dtos;

public record OrderItemDto : BaseDto
{
    public Guid OrderId { get; init; }
    public Guid ProductId { get; init; }
    public int ItemCount { get; init; }
}