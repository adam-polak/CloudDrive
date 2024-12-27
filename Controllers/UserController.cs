using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CloudDrive.Controllers;

[Route("userapi")]
public class UserController : ControllerBase
{

    private UserDataController _userDataController;

    public UserController()
    {
        _userDataController = new UserDataController();
    }

    [HttpPost("login/{username}/{password}")]
    public IActionResult Login(string username, string password)
    {
        try {
            string? loginKey = _userDataController.LoginToUser(username, password);
            if(loginKey != null) {
                return Ok(loginKey);
            } else {
                return Unauthorized();
            }
        } catch {
            return BadRequest();
        }
    }

    [HttpPost("createuser/{username}/{password}")]
    public IActionResult CreateUser(string username, string password)
    {
        try {
            _userDataController.CreateUser(username, password);
            return Ok();
        } catch {
            return BadRequest();
        }
    }

    [HttpGet("checkloginkey")]
    public IActionResult LoginKey()
    {
        try {
            bool isValid = false;

            HttpRequest request = HttpContext.Request;
            if(request.Query.TryGetValue("loginkey", out StringValues loginKey))
            {
                isValid = _userDataController.ContainsLoginKey(loginKey.ToString());
            }

            if(isValid) return Ok();
            else return Unauthorized();
        } catch {
            return BadRequest();
        }
    }

}