using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token.Extensions;
using StorEsc.API.Token.Interfaces;
using StorEsc.API.ViewModels;
using StorEsc.Application.Dtos;
using StorEsc.Application.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.API.Controllers;

[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthApplicationService _authApplicationService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IAuthApplicationService authApplicationService,
        ITokenService tokenService,
        IConfiguration configuration,
        INotificationHandler<DomainNotification> domainNotificationHandler) 
        : base(domainNotificationHandler)
    {
        _authApplicationService = authApplicationService;
        _tokenService = tokenService;
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
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
        var customerDto = new CustomerDto
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            Password = viewModel.Password
        };
        
        var customerCreated = await _authApplicationService.RegisterCustomerAsync(customerDto);
        
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
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
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

    #region Admin

    [HttpPost]
    [AllowAnonymous]
    [Route("administrator/login")]
    public async Task<IActionResult> LoginAdministratorAsync([FromBody] LoginAdminViewModel viewModel)
    {
        if (ModelState.IsValid is false)
            return UnprocessableEntity(ModelState);
        
        var administrator = await _authApplicationService.AuthenticateAdministratorAsync(viewModel.Email, viewModel.Password);

        if (HasNotifications())
            return Result();
        
        return Ok(new ResultViewModel
        {
            Message = "Login successful!",
            Success = true,
            Data = new
            {
                Id = administrator.Value.Id,
                FirstName = administrator.Value.FirstName,
                LastName =  administrator.Value.LastName,
                Email = administrator.Value.Email,
                Token = _tokenService.GenerateAdministratorToken(administrator.Value)
            }
        });
    }
    #endregion
}