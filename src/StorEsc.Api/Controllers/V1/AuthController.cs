using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StorEsc.Api.Controllers;

[Route("api/v1/[controller]")]
public class AuthController : BaseController
{

    #region Customer

    [HttpPost]
    [Route("customer/register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCustomerAsync()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("customer/login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginCustomerAsync()
    {
        return Ok();
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