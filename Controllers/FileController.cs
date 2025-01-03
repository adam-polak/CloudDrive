using Microsoft.AspNetCore.Mvc;
using CloudDrive.DataAccess.Controllers;

namespace CloudDrive.Controllers;

[Route("fileapi")]
public class FileController : ControllerBase
{

    private FileDataController _fileDataController;

    public FileController()
    {
        _fileDataController = new FileDataController();
    }

    [HttpGet("getfiles")]
    public IActionResult GetFiles()
    {
        // TODO
        return Ok();
    }

    [HttpPost("uploadfile")]
    public IActionResult UploadFile()
    {
        // TODO
            // - upload to blob first, then create in db
        return Ok();
    }

    [HttpPost("deletefile")]
    public IActionResult DeleteFile()
    {
        // TODO
        return Ok();
    }

}
