namespace StorEsc.Application.Dtos;

public class AdministratorDto : AccountDto
{
    public Guid CreatedBy { get; set; }
    public bool IsEnabled { get; set; }
}