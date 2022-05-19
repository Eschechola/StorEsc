using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StorEsc.Api.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    [Route("/")]
    [AllowAnonymous]
    public IActionResult Home()
        => Ok("It's running!");
}