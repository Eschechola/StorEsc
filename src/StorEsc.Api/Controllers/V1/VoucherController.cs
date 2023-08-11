using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorEsc.API.Token;
using StorEsc.API.Token.Extensions;

namespace StorEsc.API.Controllers;

[Route("api/v1/[controller]")]
public class VoucherController : BaseController
{
    [Route("my-vouchers")]
    [Authorize(Roles = Roles.Seller)]
    public async Task<IActionResult> GetVouchersAsync()
    {
        var sellerId = User.GetId();
        
        if (HasNotifications())
            return Result();
        
        return Ok();
    }
}