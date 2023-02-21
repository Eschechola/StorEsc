using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
using StorEsc.API.Token.Extensions;
using StorEsc.API.ViewModels;
using StorEsc.Application.DTOs;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.API.Controllers;

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
    [Route("get-last-products")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLastProducts()
    {
        var products = await _productApplicationService.GetLastProductsAsync();

        return Ok(new ResultViewModel
        {
            Message = "Products found with success",
            Success = true,
            Data = products
        });
    }
    
    [HttpGet]
    [Route("get-seller-products/{sellerId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSellerProductsAsync([FromRoute] string sellerId)
    {
        var products = await _productApplicationService.GetSellerProductsAsync(sellerId);

        if (products.Any() is false)
            return NoContent();

        return Ok(new ResultViewModel
        {
            Message = "Products found with success.",
            Success = true,
            Data = products
        });
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("search-by-name")]
    public async Task<IActionResult> SearchByProductName([FromQuery] string name)
    {
        if (string.IsNullOrEmpty(name))
            return Ok(new ResultViewModel
            {
                Message = "Name cannot be empty.",
                Success = false,
            });
        
        var products = await _productApplicationService.SearchProductsByName(name);

        if (products.Any() is false)
            return NoContent();

        return Ok(new ResultViewModel
        {
            Message = "Products found with success!",
            Success = true,
            Data = products
        });
    }

    [HttpPost]
    [Authorize(Roles = Roles.Seller)]
    [Route("create-product")]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductViewModel viewModel)
    {
        if (ModelState.IsValid is false)
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
            Data = productCreated.Value
        });
    }
}