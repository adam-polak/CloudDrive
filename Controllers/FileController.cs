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
        // folderid, file name
        // TODO
            // - check if db contains name
            // - create in db
                // - if fails, delete in db
            // - create in blob ({folderid}_{fileid}.json)
        return Ok();
    }

    [HttpPost("deletefile")]
    public IActionResult DeleteFile()
    {
        // TODO
        return Ok();
    }

}
