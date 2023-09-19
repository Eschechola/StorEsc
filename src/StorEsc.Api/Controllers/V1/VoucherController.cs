using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
using StorEsc.API.Token.Extensions;
using StorEsc.API.ViewModels;
using StorEsc.Application.Dtos;
using StorEsc.ApplicationServices.Interfaces;
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
    [Route("get-all-vouchers")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> GetVouchersAsync()
    {
        var administratorId = User.GetId();
        var vouchers = await _voucherApplicationService.GetAllVouchersAsync(administratorId);
        
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
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> CreateVoucherAsync([FromBody] CreateVoucherViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
        var sellerId = User.GetId();

        var voucherDto = new VoucherDto()
        {
            Code = viewModel.Code,
            ValueDiscount = viewModel.ValueDiscount,
            PercentageDiscount = viewModel.PercentageDiscount,
            IsPercentageDiscount = viewModel.IsPercentageDiscount
        };

        var voucherCreated = await _voucherApplicationService.CreateVoucherAsync(sellerId, voucherDto);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Voucher created with success!",
            Success = true,
            Data = voucherCreated.Value
        });
    }
}