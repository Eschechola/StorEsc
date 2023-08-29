namespace StorEsc.Application.Dtos;

public class ProductDto : BaseDto
{
    public Guid SellerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool Enabled { get; set; }
}