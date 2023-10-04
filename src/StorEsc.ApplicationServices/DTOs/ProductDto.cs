namespace StorEsc.Application.Dtos;

public record ProductDto : BaseDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public bool Enabled { get; init; }
}