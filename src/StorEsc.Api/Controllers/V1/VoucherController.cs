using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
using StorEsc.API.Token.Extensions;
using StorEsc.API.ViewModels;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.API.Controllers;

[Route("api/v1/[controller]")]
public class VoucherController : BaseController
{
    private readonly IVoucherApplicationService _voucherApplicationService;
    
    public VoucherController(
        IVoucherApplicationService voucherApplicationService,
        INotificationHandler<DomainNotification> domainNotificationHandler) : base(domainNotificationHandler)
    {
        _voucherApplicationService = voucherApplicationService;
    }
    
    [HttpGet]
    [Route("my-vouchers")]
    [Authorize(Roles = Roles.Seller)]
    public async Task<IActionResult> GetVouchersAsync()
    {
        var sellerId = User.GetId();
        var vouchers = await _voucherApplicationService.GetSellerVouchersAsync(sellerId);
        
        if (HasNotifications())
            return Result();

        if (vouchers.Any())
            return Ok(new ResultViewModel
            {
                Message = "Vouchers found with success",
                Success = true,
                Data = vouchers
            });
        
        return NoContent();
    }

    [HttpPost]
    [Route("create")]
    [Authorize(Roles = Roles.Seller)]
    public async Task<IActionResult> CreateVoucherAsync()
    {
        var sellerId = User.GetId();

        
        if (HasNotifications())
            return Result();

        return Ok();
    }
}