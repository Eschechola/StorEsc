﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
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
        var vouchers = await _voucherApplicationService.GetAllVouchersAsync();
        
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
        
        var voucherDto = new VoucherDto()
        {
            Code = viewModel.Code,
            ValueDiscount = viewModel.ValueDiscount,
            PercentageDiscount = viewModel.PercentageDiscount,
            IsPercentageDiscount = viewModel.IsPercentageDiscount
        };

        var voucherCreated = await _voucherApplicationService.CreateVoucherAsync(voucherDto);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Voucher created with success!",
            Success = true,
            Data = voucherCreated.Value
        });
    }
    
    [HttpPut]
    [Route("update/{voucherId}")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> UpdateVoucherAsync([FromRoute] string voucherId, [FromBody] UpdateVoucherViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
        var voucherDto = new VoucherDto()
        {
            Code = viewModel.Code,
            ValueDiscount = viewModel.ValueDiscount,
            PercentageDiscount = viewModel.PercentageDiscount,
            IsPercentageDiscount = viewModel.IsPercentageDiscount
        };

        var voucherUpdated = await _voucherApplicationService.UpdateVoucherAsync(voucherId, voucherDto);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Voucher updated with success!",
            Success = true,
            Data = voucherUpdated.Value
        });
    }

    [HttpPatch]
    [Authorize(Roles = Roles.Administrator)]
    [Route("enable/{voucherId}")]
    public async Task<IActionResult> EnableVoucherAsync([FromRoute] string voucherId)
    {
        await _voucherApplicationService.EnableVoucherAsync(voucherId);
        
        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Voucher enabled with success!",
            Success = true,
            Data = null
        });
    }
    
    [HttpPatch]
    [Authorize(Roles = Roles.Administrator)]
    [Route("disable/{voucherId}")]
    public async Task<IActionResult> DisableVoucherAsync([FromRoute] string voucherId)
    {
        await _voucherApplicationService.DisableVoucherAsync(voucherId);
        
        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Voucher disabled with success!",
            Success = true,
            Data = null
        });
    }
}