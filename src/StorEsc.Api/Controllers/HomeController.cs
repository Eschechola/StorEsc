using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StorEsc.API.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    [Route("/")]
    [AllowAnonymous]
    public IActionResult Home()
        => Ok(new { Message = "It's running!"});
}