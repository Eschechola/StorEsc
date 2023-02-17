using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.API.Controllers;

[Route("api/v1/[controller]")]
public class AdministratorController : BaseController
{
    private readonly IAdministratorApplicationService _admnistratorApplicationService;
    
    public AdministratorController(
        IAdministratorApplicationService administratorApplicationService,
        INotificationHandler<DomainNotification> domainNotificationHandler) : base(domainNotificationHandler)
    {
        _admnistratorApplicationService = administratorApplicationService;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("enable-default-administrator")]
    public async Task<IActionResult> EnableDefaultAdministratorAccount()
    {   
        await _admnistratorApplicationService.EnableDefaultAdministratorAsync();
        return Ok();
    }
}