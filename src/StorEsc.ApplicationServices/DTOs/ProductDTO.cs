namespace StorEsc.Application.DTOs;

public class ProductDTO : BaseDTO
{
    public Guid SellerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}