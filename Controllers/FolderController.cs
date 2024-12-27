using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CloudDrive.Controllers;

[Route("folderapi")]
public class FolderController : ControllerBase
{
    private FolderDataController _folderDataController;
    private UserDataController _userDataController;

    public FolderController()
    {
        _folderDataController = new FolderDataController();
        _userDataController = new UserDataController();
    }

    [HttpPost("createfolder")]
    public IActionResult CreateFolder()
    {
        HttpRequest request = HttpContext.Request;

        if(!request.Query.TryGetValue("loginkey", out StringValues values)) {
            throw new InvalidOperationException(
                "LoginMiddleware shouldn't allow program to this point if loginkey isn't present in query params"
                );
        }

        string loginKey = values.ToString();
        int userId = _userDataController.GetUserId(loginKey);

        if(!request.Query.TryGetValue("foldername", out StringValues value)) {
            return BadRequest();
        }

        string folderName = value.ToString();

        if(!request.Query.TryGetValue("folderid", out value)) {
            return BadRequest();
        }

        try {
            int parentId = int.Parse(value.ToString());
            if(_folderDataController.ContainsFolder(parentId, folderName)) {
                return Conflict();
            }

            _folderDataController.CreateFolder(userId, parentId, folderName);

            return Ok();
        } catch {
            return BadRequest();
        }
    }
}