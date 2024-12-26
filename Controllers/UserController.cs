using CloudDrive.DataAccess.Controllers;
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
        UserDataController userController = new UserDataController();
        userController.CorrectLogin(123);
        return Ok();
    }

    [HttpGet("temp")]
    public IActionResult Temp()
    {
        return Ok();
    }
}