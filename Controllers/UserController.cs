using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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

    [HttpGet("checkloginkey")]
    public IActionResult LoginKey()
    {
        bool isValid = false;

        HttpRequest request = HttpContext.Request;
        if(request.Query.TryGetValue("loginkey", out StringValues loginKey))
        {
            UserDataController userData = new UserDataController();
            isValid = userData.ContainsLoginKey(loginKey.ToString());
        }

        if(isValid) return Ok();
        else return Unauthorized();
    }

    [HttpGet("temp")]
    public IActionResult Temp()
    {
        return Ok();
    }
}