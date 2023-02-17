namespace StorEsc.Application.Interfaces;

public interface IAdministratorApplicationService
{
    Task<bool> EnableDefaultAdministratorAsync();
}