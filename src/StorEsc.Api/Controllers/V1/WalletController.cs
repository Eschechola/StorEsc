using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Api.Token;
using StorEsc.Api.Token.Extensions;
using StorEsc.Api.ViewModels;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Api.Controllers;

[Route("api/v1/[controller]")]
public class WalletController : BaseController
{
    private readonly IWalletApplicationService _walletApplicationService;
    
    public WalletController(
        IWalletApplicationService walletApplicationService,
        INotificationHandler<DomainNotification> domainNotificationHandler) 
        : base(domainNotificationHandler)
    {
        _walletApplicationService = walletApplicationService;
    }

    [HttpGet]
    [Authorize(Roles = Roles.Customer)]
    [Route("customer/get-wallet")]
    public async Task<IActionResult> GetCustomerWallet()
    {
        var customerId = HttpContext.User.GetId();

        var wallet = await _walletApplicationService.GetCustomerWalletAsync(customerId);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Wallet found with success!",
            Success = true,
            Data = new
            {
                Id = wallet.Id,
                Amount = wallet.Amount
            }
        });
    }
    
    [HttpGet]
    [Authorize(Roles = Roles.Seller)]
    [Route("seller/get-wallet")]
    public async Task<IActionResult> GetSellerWallet()
    {
        var customerId = HttpContext.User.GetId();

        var wallet = await _walletApplicationService.GetSellerWalletAsync(customerId);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Wallet found with success!",
            Success = true,
            Data = new
            {
                Id = wallet.Id,
                Amount = wallet.Amount
            }
        });
    }
}