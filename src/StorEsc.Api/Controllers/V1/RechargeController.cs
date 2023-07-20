using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
using StorEsc.API.Token.Extensions;
using StorEsc.API.ViewModels;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.API.Controllers;

[Route("api/v1/[controller]")]
public class RechargeController : BaseController
{
    private readonly IRechargeApplicationService _rechargeApplicationService;
    
    public RechargeController(
        IRechargeApplicationService rechargeApplicationService,
        INotificationHandler<DomainNotification> domainNotificationHandler) 
        : base(domainNotificationHandler)
    {
        _rechargeApplicationService = rechargeApplicationService;
    }

    [HttpPost]
    [Authorize(Roles = Roles.Customer)]
    [Route("recharge-customer-wallet")]
    public async Task<IActionResult> RechargeCustomerWalletAsync([FromBody] RechargeCustomerWalletViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
        var customerId = HttpContext.User.GetId();
        var amount = viewModel.Amount;
        
        var creditCardDto = new CreditCardDto
        {
            HoldName = viewModel.HoldName,
            ExpirationDate = viewModel.ExpirationDate,
            Document = viewModel.Document,
            Number = viewModel.Number,
            CVV = viewModel.CVV
        };

        await _rechargeApplicationService.RechargeCustomerWalletAsync(customerId, amount, creditCardDto);

        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Your payment was successful",
            Success = true,
            Data = null
        });
    }
}