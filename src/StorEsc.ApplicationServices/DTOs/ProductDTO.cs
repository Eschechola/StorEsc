namespace StorEsc.Application.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}