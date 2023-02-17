namespace StorEsc.Application.DTOs;

public class AdministratorDTO : AccountDTO
{
    public Guid CreatedBy { get; set; }
    public bool IsEnabled { get; set; }
}