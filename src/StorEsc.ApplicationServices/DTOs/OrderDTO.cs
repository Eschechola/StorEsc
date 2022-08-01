namespace StorEsc.Application.DTOs;

public class OrderDTO : BaseDTO
{
    public Guid CustomerId { get; set; }
    public Guid? VoucherId { get; set; }
    public decimal TotalValue { get; set;  }
    public bool IsPaid { get; set; }
    public CustomerDTO Customer { get; set; }
    public VoucherDTO Voucher { get; set; }
    public IList<OrderItemDTO> OrderItens { get; set; }
}