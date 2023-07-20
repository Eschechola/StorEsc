namespace StorEsc.Application.Dtos;

public class OrderDto : BaseDto
{
    public Guid CustomerId { get; set; }
    public Guid? VoucherId { get; set; }
    public decimal TotalValue { get; set;  }
    public bool IsPaid { get; set; }
    public CustomerDto Customer { get; set; }
    public VoucherDto Voucher { get; set; }
    public IList<OrderItemDto> OrderItens { get; set; }
}