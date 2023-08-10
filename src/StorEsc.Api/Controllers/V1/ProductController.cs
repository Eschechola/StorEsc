using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
using StorEsc.API.Token.Extensions;
using StorEsc.API.ViewModels;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;
using StorEsc.Core.Enums;

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
    [AllowAnonymous]
    [Route("search")]
    public async Task<IActionResult> SearchProductsAsync(
        [FromQuery] string sellerId = "",
        [FromQuery] string name = "",
        [FromQuery] decimal minimumPrice = 0,
        [FromQuery] decimal maximumPrice = 1_000_000,
        [FromQuery] OrderBy orderBy = OrderBy.CreatedAtDescending)
    {
        var products = await _productApplicationService.SearchProductsAsync(
            sellerId,
            name,
            minimumPrice,
            maximumPrice,
            orderBy);

        if (HasNotifications())
            return Result();
        
        if (products.Any() is false)
            return NoContent();

        return Ok(new ResultViewModel
        {
            Message = "Products found with success!",
            Success = true,
            Data = products
        });
    }

    [HttpPatch]
    [Authorize(Roles = Roles.Seller)]
    [Route("update/{productId}")]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] string productId, [FromBody] UpdateProductViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);

        var sellerId = HttpContext.User.GetId();

        var productDto = new ProductDto
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Stock = viewModel.Stock
        };

        var productUpdated = await _productApplicationService.UpdateProductAsync(productId, sellerId, productDto);
        
        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Product updated with success.",
            Success = true,
            Data = productUpdated.Value
        });
    }

    [HttpPost]
    [Authorize(Roles = Roles.Seller)]
    [Route("create")]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
        var sellerId = HttpContext.User.GetId();
        
        var productDto = new ProductDto
        {
            SellerId = Guid.Parse(sellerId),
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Stock = viewModel.Stock
        };
        
        var productCreated = await _productApplicationService.CreateProductAsync(productDto);
        
        if (HasNotifications())
            return Result();

        return Created(new ResultViewModel
        {
            Message = "Product created with success.",
            Success = true,
            Data = productCreated.Value
        });
    }

    [HttpPatch]
    [Authorize(Roles = Roles.Seller)]
    [Route("disable/{productId}")]
    public async Task<IActionResult> DisableProductAsync([FromRoute] string productId)
    {
        var sellerId = HttpContext.User.GetId();

        if (string.IsNullOrEmpty(productId))
        {
            return BadRequest(new ResultViewModel
            {
                Message = "Product Id can not be null or empty",
                Success = false,
                Data = null
            });
        }

        await _productApplicationService.DisableProductAsync(productId, sellerId);

        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Product disabled with success",
            Success = true,
            Data = null
        });
    }
    
    [HttpPatch]
    [Authorize(Roles = Roles.Seller)]
    [Route("enable/{productId}")]
    public async Task<IActionResult> EnableProductAsync([FromRoute] string productId)
    {
        var sellerId = HttpContext.User.GetId();

        if (string.IsNullOrEmpty(productId))
        {
            return BadRequest(new ResultViewModel
            {
                Message = "Product Id can not be null or empty",
                Success = false,
                Data = null
            });
        }

        await _productApplicationService.EnableProductAsync(productId, sellerId);

        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Product enabled with success",
            Success = true,
            Data = null
        });
    }
}