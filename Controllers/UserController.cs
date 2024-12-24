using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

public class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }
}