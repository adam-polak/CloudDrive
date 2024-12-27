using CloudDrive.DataAccess.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CloudDrive.Controllers;

[Route("folderapi")]
public class FolderController : ControllerBase
{
    private FolderDataController _folderDataController;

    public FolderController()
    {
        _folderDataController = new FolderDataController();
    }

    [HttpPost("createfolder")]
    public IActionResult CreateFolder()
    {
        return Ok();
    }
}