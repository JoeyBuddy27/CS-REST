using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    // Put whatever error handling you want here
    public IActionResult Error() => Problem();
}