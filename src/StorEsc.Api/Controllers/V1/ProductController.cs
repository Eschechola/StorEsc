using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Api.ViewModels;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Api.Controllers;

[Route("api/v1/[controller]")]
public class ProductController : BaseController
{
    private readonly IProductApplicationService _productApplicationService;
    
    public ProductController(
        IProductApplicationService productApplicationService,
        INotificationHandler<DomainNotification> domainNotificationHandler) 
        : base(domainNotificationHandler)
    {
        _productApplicationService = productApplicationService;
    }

    [HttpGet]
    [Route("get-seller-products/{sellerId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSellerProducts([FromQuery] string sellerId)
    {
        var products = await _productApplicationService.GetProductsAsync(sellerId);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Products found with success.",
            Success = true,
            Data = products.Value
        });
    }
}