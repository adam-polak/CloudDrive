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

}
