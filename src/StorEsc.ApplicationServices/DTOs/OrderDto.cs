namespace StorEsc.Application.Dtos;

public record OrderDto : BaseDto
{
    public Guid CustomerId { get; init; }
    public Guid? VoucherId { get; init; }
    public decimal TotalValue { get; init;  }
    public bool IsPaid { get; init; }
    public CustomerDto Customer { get; init; }
    public VoucherDto Voucher { get; init; }
    public IList<OrderItemDto> OrderItens { get; init; }
}