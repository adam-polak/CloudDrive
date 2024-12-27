using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CloudDrive.Controllers;

[Route("userapi")]
public class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpPost("login/{username}/{password}")]
    public IActionResult Login(string username, string password)
    {
        UserDataController userDataController = new UserDataController();
        try {
            string? loginKey = userDataController.LoginToUser(username, password);
            if(loginKey != null) {
                return Ok(loginKey);
            } else {
                return Unauthorized();
            }
        } catch {
            return BadRequest();
        }
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