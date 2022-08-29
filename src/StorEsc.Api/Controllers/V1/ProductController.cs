using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Api.Token;
using StorEsc.Api.Token.Extensions;
using StorEsc.Api.ViewModels;
using StorEsc.Application.DTOs;
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
    public async Task<IActionResult> GetSellerProductsAsync([FromRoute] string sellerId)
    {
        var products = await _productApplicationService.GetProductsAsync(sellerId);

        if (!products.Any())
            return NoContent();

        return Ok(new ResultViewModel
        {
            Message = "Products found with success.",
            Success = true,
            Data = products
        });
    }

    [HttpPost]
    [Authorize(Roles = Roles.Seller)]
    [Route("create-product")]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var sellerId = HttpContext.User.GetId();
        
        var productDTO = new ProductDTO
        {
            SellerId = Guid.Parse(sellerId),
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Stock = viewModel.Stock
        };
        
        var productCreated = await _productApplicationService.CreateProductAsync(productDTO);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Product registered with success.",
            Success = true,
            Data = productCreated
        });
    }
}