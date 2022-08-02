using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Api.Token.Extensions;
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
    private readonly IConfiguration _configuration;

    public AuthController(
        IAuthApplicationService authApplicationService,
        ITokenService tokenService,
        IConfiguration configuration,
        INotificationHandler<DomainNotification> domainNotificationHandler) 
        : base(domainNotificationHandler)
    {
        _authApplicationService = authApplicationService;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    #region Me

    [HttpGet]
    [Authorize]
    [Route("me")]
    public IActionResult Me()
    {
        var user = HttpContext.User;
        
        return Ok(new
        {
            Id = user.GetId(),
            Email = user.GetEmail(),
            Roles = user.GetRole(),
            Jti = user.GeTokenIdentifier(),
            Nbf = user.GetTokenNotBefore(),
            Iat = user.GetTokenIssuedAt()
        });
    }

    #endregion

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
                FirstName = customerCreated.Value.FirstName,
                LastName =  customerCreated.Value.LastName,
                Email = customerCreated.Value.Email,
                Wallet = new
                {
                    Id = customerCreated.Value.Wallet.Id,
                    Amount = customerCreated.Value.Wallet.Amount
                },
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
                FirstName = customer.Value.FirstName,
                LastName =  customer.Value.LastName,
                Email = customer.Value.Email,
                Wallet = new
                {
                    Id = customer.Value.Wallet.Id,
                    Amount = customer.Value.Wallet.Amount
                },
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
    [Route("seller/login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginSellerAsync([FromBody]LoginSellerViewModel viewModel)
    {
        var seller = await _authApplicationService.AuthenticateSellerAsync(viewModel.Email, viewModel.Password);
        
        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Login successful!",
            Success = true,
            Data = new
            {
                Id = seller.Value.Id,
                FirstName = seller.Value.FirstName,
                LastName =  seller.Value.LastName,
                Email = seller.Value.Email,
                Wallet = new
                {
                    Id = seller.Value.Wallet.Id,
                    Amount = seller.Value.Wallet.Amount
                },
                Token = _tokenService.GenerateSellerToken(seller.Value)
            }
        });
    }
    
    [HttpPost]
    [Route("seller/register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterSellerAsync([FromBody] RegisterSellerViewModel viewModel)
    {
        var sellerDTO = new SellerDTO
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            Password = viewModel.Password
        };
        
        var sellerCreated = await _authApplicationService.RegisterSellerAsync(sellerDTO);
        
        if (HasNotifications())
            return Result();
        
        return Created(new ResultViewModel
        {
            Message = "Seller created with success!",
            Success = true,
            Data = new
            {
                Id = sellerCreated.Value.Id,
                FirstName = sellerCreated.Value.FirstName,
                LastName =  sellerCreated.Value.LastName,
                Email = sellerCreated.Value.Email,
                Wallet = new
                {
                    Id = sellerCreated.Value.Wallet.Id,
                    Amount = sellerCreated.Value.Wallet.Amount
                },
                Token = _tokenService.GenerateSellerToken(sellerCreated.Value)
            }
        });
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