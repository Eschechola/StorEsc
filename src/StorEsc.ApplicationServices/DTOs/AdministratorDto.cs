namespace StorEsc.Application.Dtos;

public record AdministratorDto : AccountDto
{
    public Guid CreatedBy { get; init; }
    public bool IsEnabled { get; init; }
}