using CloudDrive.DataAccess.Controllers;
using CloudDrive.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace CloudDrive.Controllers;

[Route("userapi")]
public class UserController : ControllerBase
{

    private UserDataController _userDataController;
    private FolderDataController _folderDataController;

    public UserController()
    {
        _userDataController = new UserDataController();
        _folderDataController = new FolderDataController();
    }

    [HttpPost("login/{username}/{password}")]
    public IActionResult Login(string username, string password)
    {
        try {
            string? loginKey = _userDataController.LoginToUser(username, password);
            if(loginKey != null) {
                User user = _userDataController.GetUser(loginKey);

                Folder? root = _folderDataController.GetRootFolder(user.Id);
                if(root == null)
                {
                    _folderDataController.CreateRootFolder(user.Id);
                    root = _folderDataController.GetRootFolder(user.Id);
                    if(root == null)
                    {
                        return BadRequest("Error creating root folder");
                    }
                }

                int rootFolderId = root.Id;
                object response = new { LoginKey = loginKey, RootFolderId = rootFolderId };
                return Ok(JsonConvert.SerializeObject(response));
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
            if(_userDataController.ContainsUsername(username)) {
                return Conflict();
            }
            _userDataController.CreateUser(username, password);
            int userId = _userDataController.GetUserId(username, password);
            // Create root folder
            _folderDataController.CreateRootFolder(userId);

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