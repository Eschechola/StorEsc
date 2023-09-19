using StorEsc.Application.Interfaces;
using StorEsc.DomainServices.Interfaces;

namespace StorEsc.ApplicationServices.Services;

public class AdministratorApplicationService : IAdministratorApplicationService
{
    private readonly IAdministratorDomainService _administratorDomainService;

    public AdministratorApplicationService(IAdministratorDomainService administratorDomainService)
    {
        _administratorDomainService = administratorDomainService;
    }

    public async Task<bool> EnableDefaultAdministratorAsync()
        => await _administratorDomainService.EnableDefaultAdministratorAsync();
}