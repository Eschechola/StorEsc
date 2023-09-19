namespace StorEsc.ApplicationServices.Interfaces;

public interface IAdministratorApplicationService
{
    Task<bool> EnableDefaultAdministratorAsync();
}