using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CloudDrive.Controllers;

[Route("folderapi")]
public class FolderController : ControllerBase
{
    private FolderDataController _folderDataController;
    private UserDataController _userDataController;
    private string _loginKey;
    private int _userId;
    private HttpRequest _request;

    public FolderController()
    {
        _folderDataController = new FolderDataController();
        _userDataController = new UserDataController();
        
        _request = HttpContext.Request;

        if(!_request.Query.TryGetValue("loginkey", out StringValues values)) {
            throw new InvalidOperationException(
                "LoginMiddleware shouldn't allow program to this point if loginkey isn't present in query params"
                );
        }

        _loginKey = values.ToString();
        _userId = _userDataController.GetUserId(_loginKey);
    }

    [HttpPost("createfolder")]
    public IActionResult CreateFolder()
    {
        if(!_request.Query.TryGetValue("foldername", out StringValues value)) {
            return BadRequest();
        }

        string folderName = value.ToString();

        if(!_request.Query.TryGetValue("folderid", out value)) {
            return BadRequest();
        }

        try {
            int parentId = int.Parse(value.ToString());
            if(_folderDataController.ContainsFolder(parentId, folderName)) {
                return Conflict();
            }

            _folderDataController.CreateFolder(_userId, parentId, folderName);
            
            return Ok();
        } catch {
            return BadRequest();
        }
    }
}