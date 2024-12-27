using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CloudDrive.Controllers;

[Route("folderapi")]
public class FolderController : ControllerBase
{
    private FolderDataController _folderDataController;
    private string _loginKey;
    private HttpRequest _request;

    public FolderController()
    {
        _folderDataController = new FolderDataController();
        
        _request = HttpContext.Request;

        if(request.Query.TryGetValue("loginkey", out StringValues values)) {
            _loginKey = values.ToString();
        } else {
            throw new InvalidOperationException(
                "LoginMiddleware shouldn't allow program to this point if loginkey isn't present in query params"
                );
        }
    }

    [HttpPost("createfolder")]
    public IActionResult CreateFolder()
    {
        if(!_request.Query.TryGetValue("foldername", out StringValues value)) {
            return BadRequest();
        }

        string folderName = value.ToString();

        try {
            
        } catch {
            return BadRequest();
        }
    }
}