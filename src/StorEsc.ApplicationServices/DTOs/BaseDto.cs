namespace StorEsc.Application.Dtos;

public abstract record BaseDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}