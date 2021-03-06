using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Api.Token.Interfaces;
using StorEsc.Api.ViewModels;
using StorEsc.Application.DTOs;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Api.Controllers;

[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthApplicationService _authApplicationService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IAuthApplicationService authApplicationService,
        ITokenService tokenService,
        INotificationHandler<DomainNotification> domainNotificationHandler) : base(domainNotificationHandler)
    {
        _authApplicationService = authApplicationService;
        _tokenService = tokenService;
    }

    #region Customer

    [HttpPost]
    [AllowAnonymous]
    [Route("customer/register")]
    public async Task<IActionResult> RegisterCustomerAsync([FromBody] RegisterCustomerViewModel viewModel)
    {
        var customerDTO = new CustomerDTO
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            Password = viewModel.Password
        };
        
        var customerCreated = await _authApplicationService.RegisterCustomerAsync(customerDTO);
        
        if (HasNotifications())
            return Result();
        
        return Created(new ResultViewModel
        {
            Message = "Customer created with success!",
            Success = true,
            Data = new
            {
                Id = customerCreated.Value.Id,
                WalletId = customerCreated.Value.WalletId,
                FirstName = customerCreated.Value.FirstName,
                LastName =  customerCreated.Value.LastName,
                Email = customerCreated.Value.Email,
                Token = _tokenService.GenerateCustomerToken(customerCreated.Value)
            }
        });
    }
    
    [HttpPost]
    [AllowAnonymous]
    [Route("customer/login")]
    public async Task<IActionResult> LoginCustomerAsync([FromBody] LoginCustomerViewModel viewModel)
    {
        var customer = await _authApplicationService.AuthenticateCustomerAsync(viewModel.Email, viewModel.Password);
        
        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Login successful!",
            Success = true,
            Data = new
            {
                Id = customer.Value.Id,
                WalletId = customer.Value.WalletId,
                FirstName = customer.Value.FirstName,
                LastName =  customer.Value.LastName,
                Email = customer.Value.Email,
                Token = _tokenService.GenerateCustomerToken(customer.Value)
            }
        });
    }
    
    [HttpPost]
    [Route("customer/forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordCustomerAsync()
    {
        return Ok();
    }

    #endregion

    #region Seller

    [HttpPost]
    [Route("seller/register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterSellerAsync()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("seller/login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginSellerAsync()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("seller/forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordSellerAsync()
    {
        return Ok();
    }

    #endregion
}